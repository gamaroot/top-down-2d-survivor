using Database;
using UnityEngine;

namespace Game
{
    internal class BulletPool : MonoBehaviour
    {
        [SerializeField] private UpgradeItemDatabase _database;

        private static Pool[] _bulletPools;
        private static Pool[] _impactPools;

        private void Start()
        {
            int totalBullets = this._database.Weapons.Length;

            _bulletPools = new Pool[totalBullets];
            _impactPools = new Pool[totalBullets];

            for (int index = 0; index < totalBullets; index++)
            {
                WeaponData data = this._database.Weapons[index];
                _impactPools[index] = new Pool(base.transform, data.ImpactPrefab);
                _bulletPools[index] = new Pool(base.transform, data.BulletPrefab,
                (obj) =>
                {
                    CircleCollider2D collider = obj.AddComponent<CircleCollider2D>();
                    collider.isTrigger = true;

                    obj.AddComponent<Bullet>();
                },
                (obj) =>
                {
                    Bullet bullet = obj.GetComponent<Bullet>();
                    bullet.Damage = data.GetDamage();
                });
            }
        }

        internal static Transform SpawnBullet(int id, Vector2 spawnPoint)
        {
            Pool bulletPool = _bulletPools[id];

            Bullet bullet = bulletPool.BorrowMeObjectFromPool<Bullet>();
            bullet.transform.position = spawnPoint;
            bullet.OnHit = (hitPoint) =>
            {
                Pool impactPool = _impactPools[id];

                impactPool.Spawn(1f).position = hitPoint;
            };
            bullet.gameObject.SetActive(true);

            return bullet.transform;
        }
    }
}