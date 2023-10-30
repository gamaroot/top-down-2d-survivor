using Database;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    internal class Weaponry : MonoBehaviour
    {
        private const float ORBITAL_DURATION = 0.3f;

        [SerializeField] private UpgradeItemDatabase _database;
        [SerializeField] private EnemyDetector _enemyDetector;
        
        private readonly List<Weapon> _orbitalWeapons = new();

        private void Awake()
        {
            this.LoadPrimaryWeapon();

            UpgradeItemData.OnLevelChange += this.AttachOrbitalWeapon;

            for (int index = 0; index < this._database.Weapons.Length; index++)
                this.AttachOrbitalWeapon(this._database.Weapons[index]);
        }

        private void AttachOrbitalWeapon(UpgradeItemData data)
        {
            if (data.ID == 0 ||
                data.Type != UpgradeType.WEAPON ||
                data.Level < 1 ||
                this._orbitalWeapons.Any(x => x.ID == data.ID))
            {
                return;
            }

            var weaponData = data as WeaponData;

            GameObject weaponObj = Instantiate(weaponData.BulletPrefab);
            Weapon weapon = weaponObj.AddComponent<Weapon>();
            weapon.Setup(weaponData.ID, weaponData.GetBulletSpeed(), weaponData.ShootInterval, this._enemyDetector);

            this._orbitalWeapons.Add(weapon);

            Transform container = new GameObject("Orbital Container").transform;
            container.position = base.transform.position;
            container.SetParent(base.transform);

            weapon.transform.SetParent(container, true);

            Transform orbitalWeapon = weaponObj.transform;

            orbitalWeapon.DOLocalMoveX(weaponData.OrbitalRadius, ORBITAL_DURATION);

            Transform orbitalContainer = orbitalWeapon.parent;
            Vector3 targetAngles = orbitalContainer.eulerAngles;
            targetAngles.z += 360f;

            orbitalContainer.DORotate(targetAngles,
                                      weaponData.OrbitalSpeed,
                                      RotateMode.FastBeyond360)
                                .SetEase(Ease.Linear)
                                .SetSpeedBased(true)
                                .SetLoops(-1, LoopType.Restart);
        }

        private void LoadPrimaryWeapon()
        {
            WeaponData primaryWeapon = this._database.Weapons[0];
            if (primaryWeapon.Level == 0)
                primaryWeapon.Level = 1;

            Weapon weapon = new GameObject("Primary Weapon").AddComponent<Weapon>();
            weapon.Setup(primaryWeapon.ID, primaryWeapon.GetBulletSpeed(), primaryWeapon.ShootInterval, this._enemyDetector);
            weapon.transform.SetParent(base.transform);
        }
    }
}