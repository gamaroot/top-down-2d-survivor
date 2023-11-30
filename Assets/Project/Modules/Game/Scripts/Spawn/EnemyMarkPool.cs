using UnityEngine;

namespace Game
{
    internal class EnemyMarkPool : MonoBehaviour
    {
        [SerializeField] private EnemyMark _prefab;
        private static Pool _pool;

        private void Awake()
        {
            _pool = new Pool(base.transform, this._prefab.gameObject);
        }

        internal static void Spawn(IBody playerBody, IBody enemyBody)
        {
            EnemyMark spawn = _pool.BorrowMeObjectFromPool<EnemyMark>();
            spawn.gameObject.SetActive(true);
            spawn.Setup(playerBody, enemyBody);
        }
    }
}