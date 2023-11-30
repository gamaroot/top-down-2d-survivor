using Database;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    internal class Statistics
    {
        internal int TotalMatches { get; private set; }
        internal int TotalEnemiesDestroyed { get; private set; }
        internal int HighestWave { get; private set; }
        internal int CurrentWave;

        internal Action<int> OnTotalEnemiesDestroyedChange;

        internal static void Load()
        {
            Instance = new Statistics
            {
                TotalMatches = PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_TOTAL_MATCHES_KEY),
                TotalEnemiesDestroyed = PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_TOTAL_ENEMIES_DESTROYED_KEY),
                HighestWave = PlayerPrefs.GetInt(PlayerPrefsKeys.STATISTICS_HIGHEST_WAVE_KEY)
            };
        }
        internal static Statistics Instance;

        private Statistics()
        {
            UpgradeItemData.OnLevelChange += (data) =>
            {
                this.SetHighestWave(Math.Max((int)data.GetValue(), this.HighestWave));
            };
        }

        internal void IncrementMatches()
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.STATISTICS_TOTAL_MATCHES_KEY, ++this.TotalMatches);
            PlayerPrefs.Save();
        }

        internal void IncrementWave()
        {
            if (++this.CurrentWave <= this.HighestWave)
                return;

            this.SetHighestWave(this.CurrentWave);
        }

        internal void IncrementEnemiesDestroyed()
        {
            this.OnTotalEnemiesDestroyedChange?.Invoke(++this.TotalEnemiesDestroyed);

            PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_TOTAL_ENEMIES_DESTROYED_KEY, this.TotalEnemiesDestroyed);
            PlayerPrefs.Save();
        }

        private void SetHighestWave(int wave)
        {
            this.HighestWave = wave;

            PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_HIGHEST_WAVE_KEY, this.HighestWave);
            PlayerPrefs.Save();
        }

        internal int ResetCurrentWave() => this.CurrentWave = 0;
    }
}