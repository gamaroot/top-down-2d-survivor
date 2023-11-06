using Game;
using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace Database
{
    public class EnemyDatabase : ScriptableObject
    {
        [field: SerializeField] public float BaseHealth { get; private set; } = 3f;

        [field: SerializeField] public float BaseDamage { get; private set; } = 10f;

        [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;

        [field: SerializeField] public GameObject[] Enemies { get; private set; }

        public int GetDamage(int level)
        {
            return (int)Math.Max(this.BaseDamage, this.BaseDamage * (float)Math.Pow(level, 1.01d));
        }
        public int GetHealth(int level)
        {
            return (int)(Math.Max(this.BaseHealth, this.BaseHealth * (float)Math.Pow(level, 2.5f)) * 2.5f);
        }

        public float GetSpeed(int level)
        {
            return Math.Min(3f, this.BaseSpeed + (0.02f * level));
        }
    }
}