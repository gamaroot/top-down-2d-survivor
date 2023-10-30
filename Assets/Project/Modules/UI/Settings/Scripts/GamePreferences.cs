using System;
using UnityEngine;

namespace Game
{
    internal class GamePreferences
    {
        private const string SOUND_LEVEL_KEY = "SOUND_LEVEL",
                             MUSIC_LEVEL_KEY = "MUSIC_LEVEL";

        internal static Action<float> OnMusicVolumeChange;

        internal static float SoundVolume
        {
            get => PlayerPrefs.GetFloat(SOUND_LEVEL_KEY, 0.7f);
            set
            {
                PlayerPrefs.SetFloat(SOUND_LEVEL_KEY, value);
                PlayerPrefs.Save();
            }
        }
        internal static float MusicVolume
        {
            get => PlayerPrefs.GetFloat(MUSIC_LEVEL_KEY, 1f);
            set
            {
                PlayerPrefs.SetFloat(MUSIC_LEVEL_KEY, value);
                PlayerPrefs.Save();

                //OnMusicVolumeChange.Invoke(value);
            }
        }
    }
}