using UnityEngine;

namespace Game
{
    internal interface IEnemyDetector
    {
        Transform FindClosestTarget();
    }
}