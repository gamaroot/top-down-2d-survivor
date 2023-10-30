using UnityEngine;
using DG.Tweening;

namespace Game
{
    internal class Weapon : MonoBehaviour
    {
        internal int ID { get; private set; }

        private float _speed;
        private float _shootInterval;
        private IEnemyDetector _enemyDetector;

        private void Start()
        {
            base.InvokeRepeating("Shoot", 0, _shootInterval);
        }

        internal void Setup(int id, float speed, float shootInterval, IEnemyDetector enemyDetector)
        {
            this.ID = id;
            this._speed = speed;
            this._shootInterval = shootInterval;
            this._enemyDetector = enemyDetector;
        }

        private void Shoot()
        {
            Transform target = this._enemyDetector.FindClosestTarget();
            if (target == null)
                return;

            Vector2 spawnPoint = base.transform.position;
            Vector2 direction = (Vector2)target.position - spawnPoint;
            Vector2 destination = spawnPoint + (direction.normalized * 100f);

            BulletPool.SpawnBullet(this.ID, spawnPoint)
                          .DOMove(destination, this._speed)
                          .SetSpeedBased(true)
                          .SetEase(Ease.Linear);
        }
    }
}