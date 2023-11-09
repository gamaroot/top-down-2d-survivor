using Database;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    internal class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyDatabase _database;
        private static List<Pool> _pools;

        private void Awake()
        {
            _pools = new List<Pool>();

            int totalObjects = this._database.Enemies.Length;

            for (int index = 0; index < totalObjects; index++)
                _pools.Add(new Pool(base.transform, this._database.Enemies[index]));
        }

        internal static Enemy Spawn(int type, Vector2 point)
        {
            Pool pool = _pools[type];

            Enemy enemy = pool.BorrowMeObjectFromPool<Enemy>();
            enemy.transform.position = point;
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        internal static void DisableAll()
        {
            _pools.ForEach(pool => pool.DisableAll());
        }
    }
}