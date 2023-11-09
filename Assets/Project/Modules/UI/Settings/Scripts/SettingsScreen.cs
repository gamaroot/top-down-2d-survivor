using ScreenNavigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    internal class SettingsScreen : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Slider _sliderSoundVolume, _sliderMusicVolume;
        [SerializeField] private TextMeshProUGUI _textSoundVolume, _textMusicVolume, _textVersion;

        private void Start()
        {
            this._sliderSoundVolume.value = GamePreferences.SoundVolume;
            this._sliderMusicVolume.value = GamePreferences.MusicVolume;

            this.OnUpdateSoundVolume(this._sliderSoundVolume.value);
            this.OnUpdateMusicVolume(this._sliderMusicVolume.value);

            this._sliderSoundVolume.onValueChanged.AddListener(this.OnUpdateSoundVolume);
            this._sliderMusicVolume.onValueChanged.AddListener(this.OnUpdateMusicVolume);

            this._textVersion.text = Application.version.ToString();

            this._animator.SetBool(AnimationParams.VISIBLE, true);

            SceneNavigator.Instance.ExecuteOnHideProcessStart((SceneID)base.gameObject.scene.buildIndex, () =>
            {
                this._animator.SetBool(AnimationParams.VISIBLE, false);
            });
        }

        internal void OnPrivacyClick()
        {
            Application.OpenURL(URL.PRIVACY);
        }

        private void OnUpdateSoundVolume(float volume)
        {
            this._textSoundVolume.text = $"{volume * 100f:N0}%";
            GamePreferences.SoundVolume = volume;
        }
        private void OnUpdateMusicVolume(float volume)
        {
            this._textMusicVolume.text = $"{volume * 100f:N0}%";
            GamePreferences.MusicVolume = volume;
        }
    }
}