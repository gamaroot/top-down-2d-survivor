using Game;
using System;
using Unity.Collections;
using UnityEngine;

namespace Database
{
    [Serializable]
    public class WeaponData : UpgradeItemData
    {
        [HideInInspector]
        public float OrbitalRadius, OrbitalSpeed;

        [field: SerializeField] public GameObject BulletPrefab { get; private set; }
        [field: SerializeField] public GameObject ImpactPrefab { get; private set; }
        [field: SerializeField] public int UnlockLevel { get; private set; }

        [field: SerializeField] public float Damage { get; private set; } = 10;

        [field: SerializeField] public float ShootInterval { get; private set; } = 1f;
        [field: SerializeField] public float BulletSpeed { get; private set; } = 3f;

        public float GetDamage()
        {
            return Math.Max(this.Damage, this.Damage * (float)Math.Pow(this.Level, 1.01d));
        }

        public float GetShootInterval()
        {
            return Math.Max(0.5f, this.ShootInterval - (Math.Max(0, this.Level - 1) / 1000F));
        }

        public float GetBulletSpeed()
        {
            return Math.Min(30, this.BulletSpeed + (Math.Max(0, this.Level - 1) / 1000F));
        }

        public override float GetValue()
        {
            return this.GetDamage() / this.GetShootInterval();
        }
    }
}