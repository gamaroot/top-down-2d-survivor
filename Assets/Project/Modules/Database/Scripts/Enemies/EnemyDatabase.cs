using Game;
using System;
using UnityEngine;

namespace Database
{
    public class EnemyDatabase : ScriptableObject
    {
        [field: SerializeField] public float BaseHealth { get; private set; } = 3f;

        [field: SerializeField] public float BaseDamage { get; private set; } = 10f;

        [field: SerializeField] public float BaseSpeed { get; private set; } = 1f;

        [field: SerializeField] public GameObject[] Enemies { get; private set; }

        public int GetDamage(int level) => (int)GameBalance.Calculate(this.BaseDamage, level);
        public int GetHealth(int level) => (int)GameBalance.CalculateEnemyHealth(this.BaseHealth, level);
        public float GetSpeed(int level) => Math.Min(3f, this.BaseSpeed + (0.02f * level));
    }
}