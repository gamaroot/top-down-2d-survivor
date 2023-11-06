using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(AudioSource))]
    internal class BGM : MonoBehaviour
    {
        private const float AUDIO_FADE_DURATION = 2f;

        [HideInInspector]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _playlist;

        private readonly List<int> _toPlay = new();

        private int _playingMusic = -1;

        private void OnValidate()
        {
            if (this._audioSource == null)
                this._audioSource = base.GetComponent<AudioSource>();
        }

        private void Start()
        {
            GamePreferences.OnMusicVolumeChange = this.OnMusicPrefChange;
            if (GamePreferences.MusicVolume == 0) return;

            this.RandomizeMusic();
        }

        private void OnMusicPrefChange(float volume)
        {
            this._audioSource.volume = volume;

            if (volume > 0 && !this._audioSource.isPlaying)
                this._audioSource.Play();
            else if (volume == 0 && this._audioSource.isPlaying)
                this._audioSource.Pause();
        }

        private void RandomizeMusic()
        {
            if (GamePreferences.MusicVolume == 0) return;

            if (this._audioSource.isPlaying)
                this._audioSource.DOFade(0, AUDIO_FADE_DURATION).OnComplete(FadeNewMusic);
            else
                FadeNewMusic();

            void FadeNewMusic()
            {
                if (this._toPlay.Count == 0)
                {
                    for (int index = 0; index < this._playlist.Length; index++)
                        this._toPlay.Add(index);

                    if (this._playingMusic >= 0)
                        this._toPlay.Remove(this._playingMusic);
                }

                this._playingMusic = this._toPlay[Random.Range(0, this._toPlay.Count)];

                this._toPlay.Remove(this._playingMusic);

                this._audioSource.clip = this._playlist[this._playingMusic];
                this._audioSource.Play();

                this._audioSource.DOFade(1f, AUDIO_FADE_DURATION);

                float remainingTime = this._audioSource.clip.length - this._audioSource.time;
                base.Invoke("RandomizeMusic", remainingTime);
            }
        }
    }
}