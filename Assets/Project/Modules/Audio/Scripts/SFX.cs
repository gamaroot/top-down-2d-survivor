using UnityEngine;

namespace Game
{
    internal class SFX : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabsShoot, _prefabsHit;

        private static Pool[] _poolsShoot, _poolsHit;

        private void Awake()
        {
            this.CreatePools(ref _poolsHit, this._prefabsHit);
            this.CreatePools(ref _poolsShoot, this._prefabsShoot);
        }

        internal static void Play(SFXType type, int id)
        {
            float volume = GamePreferences.SoundVolume;
            if (volume == 0) return;

            Pool pool = type switch
            {
                SFXType.SHOOT => _poolsShoot[id],
                _ => _poolsHit[id],
            };
            pool.PlayAudioFromPool(volume);
        }

        private void CreatePools(ref Pool[] pools, GameObject[] prefabs)
        {
            int length = prefabs.Length;

            pools = new Pool[length];
            for (int index = 0; index < length; index++)
                pools[index] = new Pool(base.transform, prefabs[index]);
        }
    }
}