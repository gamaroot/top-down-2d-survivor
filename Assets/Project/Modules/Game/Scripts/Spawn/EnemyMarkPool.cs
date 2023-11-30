using UnityEngine;
using Utils;

namespace Game
{
    internal class EnemyMarkPool : MonoBehaviour
    {
        [Tooltip("Bottom margin in pixels")]
        [SerializeField] private float _bottomMarginInPixels;
        [SerializeField] private EnemyMark _prefab;
        private static Pool _pool;

        private static float _bottomMargin;

        private void Awake()
        {
            _pool = new Pool(base.transform, this._prefab.gameObject);
        }

        private void Start()
        {
            // Considering the default's pixels per unit as 100
            _bottomMargin = this._bottomMarginInPixels / 100f;
        }

        internal static void Spawn(IBody playerBody, IBody enemyBody)
        {
            EnemyMark spawn = _pool.BorrowMeObjectFromPool<EnemyMark>();
            spawn.gameObject.SetActive(true);
            spawn.Setup(playerBody, enemyBody, _bottomMargin);
        }
    }
}