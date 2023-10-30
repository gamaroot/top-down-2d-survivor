using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    internal class EnemyDetector : MonoBehaviour, IEnemyDetector
    {
        private readonly List<Transform> _targets = new();

        private readonly object _lock = new();

        public Transform FindClosestTarget() => this._targets.Count > 0 ? this._targets[0] : null;

        private void OnTriggerEnter2D(Collider2D collider) => this.UpdateTargets(collider, true);
        private void OnTriggerExit2D(Collider2D collider) => this.UpdateTargets(collider, false);

        private void UpdateTargets(Collider2D collider, bool isToAdd)
        {
            lock (this._lock)
            {
                if (isToAdd)
                {
                    if (this._targets.Count == 0)
                    {
                        this._targets.Add(collider.transform);
                    }
                    else if (!this._targets.Contains(collider.transform))
                    {
                        Vector2 playerPosition = Player.Info.GetPosition();
                        float oldDistance = Vector2.Distance(this._targets[0].position, playerPosition);
                        float newDistance = Vector2.Distance(collider.transform.position, playerPosition);

                        if (newDistance < oldDistance)
                            this._targets.Insert(0, collider.transform);
                        else
                            this._targets.Add(collider.transform);
                    }
                }
                else
                {
                    this._targets.Remove(collider.transform);
                }
            }
        }
    }
}