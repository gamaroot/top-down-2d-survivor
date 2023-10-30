using UnityEngine;
using System.Linq;

namespace Database
{
    public class UpgradeItemDatabase : ScriptableObject
    {
        public WeaponData[] Weapons;
        public StatsData[] Stats;

        private void OnValidate()
        {
            float orbitalSpeed = 0;
            float orbitalRadius = 0;

            int index = 0;
            foreach (WeaponData weapon in this.Weapons)
            {
                weapon.ID = index++;
                weapon.OrbitalSpeed = orbitalSpeed;
                weapon.OrbitalRadius = orbitalRadius;

                orbitalSpeed += 10f;
                orbitalRadius += index == 1 ? 1f : 0.25f;
#if UNITY_EDITOR
                weapon.FACTOR = weapon.Damage / weapon.ShootInterval / weapon.BaseCost;
#endif
            }

            foreach (StatsData stats in this.Stats)
                stats.ID = index++;
        }

        public float GetStatsValue(UpgradeType type)
        {
            return this.Stats[(int)type - 1].GetValue();
        }

        public float GetDPS()
        {
            return this.Weapons.Sum(x => x.Level > 0 ? x.GetValue() : 0);
        }
    }
}