using UnityEngine;

namespace Game
{
    internal class ExplosionPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        private static Pool _pool;

        private void Awake() => _pool = new Pool(base.transform, this._prefab);

        internal static void Spawn(Vector2 point) => _pool.Spawn(1f).position = point;
    }
}