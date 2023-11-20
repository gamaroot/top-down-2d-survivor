using Database;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    internal class Statistics
    {
        internal float TotalMatches { get; private set; }
        internal float TotalEnemiesDestroyed { get; private set; }
        internal int HighestWave { get; private set; }
        internal int CurrentWave;

        internal Action<int> OnHighestWaveChange;

        internal static void Load()
        {
            Instance = new Statistics
            {
                TotalMatches = PlayerPrefs.GetFloat(PlayerPrefsKeys.STATISTICS_TOTAL_MATCHES_KEY),
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

        private void SetHighestWave(int wave)
        {
            this.HighestWave = wave;

            PlayerPrefs.SetInt(PlayerPrefsKeys.STATISTICS_HIGHEST_WAVE_KEY, this.HighestWave);
            PlayerPrefs.Save();

            OnHighestWaveChange?.Invoke(this.HighestWave);
        }

        internal int ResetCurrentWave() => this.CurrentWave = 0;
    }
}