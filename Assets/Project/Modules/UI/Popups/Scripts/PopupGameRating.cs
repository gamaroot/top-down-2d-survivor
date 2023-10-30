using I2.Loc;
using System;
using UnityEngine;

namespace Game
{
    internal class PopupGameRating : Popup
    {
        private bool hasShownNegativeGameRating;

        private void OnEnable() => this.hasShownNegativeGameRating = false;

        private static bool HasAnswered
        {
            get { return PlayerPrefs.GetInt(PlayerPrefsKeys.HAS_ANSWERED_GAME_RATING_KEY, 0) == 1; }
            set
            {
                PlayerPrefs.SetInt(PlayerPrefsKeys.HAS_ANSWERED_GAME_RATING_KEY, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }

        public void OnConfirmButtonClick()
        {
            HasAnswered = true;

            if (this.hasShownNegativeGameRating)
            {
                string subject = Uri.EscapeDataString(ScriptLocalization.PopupGameRating.EMAIL_SUBJECT);
                Application.OpenURL($"mailto:devjim2023@gmail.com?subject={subject}");
            } else {
                Application.OpenURL(URL.STORE);
            }
            base.Hide();
        }

        public void OnDenyButtonClick()
        {
            HasAnswered = true;
            if (!this.hasShownNegativeGameRating)
            {
                this.hasShownNegativeGameRating = true;
                base.Animator.SetTrigger(PlayerPrefsKeys.SHOW_NEGATIVE_GAME_RATING_TRIGGER);
            } else {
                base.Hide();
            }
        }

        internal static bool NeedToShow()
        {
            bool needToShow = false;
            if (!HasAnswered && Statistics.Instance.HighestWave >= 5)
            {
                string lastShownDateTimeAsString = PlayerPrefs.GetString(PlayerPrefsKeys.LAST_GAME_RATING_SHOWN_DATETIME_KEY, "");
                needToShow = string.IsNullOrEmpty(lastShownDateTimeAsString);
                if (!needToShow)
                {
                    var lastShownDateTime = DateTime.Parse(lastShownDateTimeAsString);
                    needToShow = (DateTime.Today - lastShownDateTime).Days >= 1;
                }

                if (needToShow)
                {
                    PlayerPrefs.SetString(PlayerPrefsKeys.LAST_GAME_RATING_SHOWN_DATETIME_KEY, DateTime.Today.ToString());
                    PlayerPrefs.Save();
                }
            }
            return needToShow;
        }
    }
}