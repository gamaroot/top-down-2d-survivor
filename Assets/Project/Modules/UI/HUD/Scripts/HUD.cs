using Database;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    internal class HUD : MonoBehaviour
    {
        private const float HEALTH_PERCENT_DISPLAY_DURATION = 1.5f;

        [SerializeField] private UpgradeItemDatabase _database;

        [SerializeField] private TextMeshProUGUI _stageLabel, _textRemainingTime, _textTotalCoins, _textDPS, _textHealthBar;

        [SerializeField] private Slider _sliderLevelBar, _sliderHealthBar;

        [SerializeField] private TextMeshPro _textPercentHealth;

        [SerializeField] private Color _levelBarStartColor, _levelBarEndColor;
        [SerializeField] private Image _levelBarFill;

        [SerializeField] private Color _damageBackgroundColor;

        private Tweener _tweenPercentHealth;

        private void Start()
        {
            this._textTotalCoins.text = Wallet.Instance.Coins.ToAlphabeticNotation();

            Wallet.Instance.OnChange += (lastCoins, currCoins) =>
            {
                this._textTotalCoins.text = currCoins.ToAlphabeticNotation();
            };

            this.UpdateDPS(this._database.GetDPS());
            WeaponData.OnLevelChange += (data) =>
            {
                if (data.Type == UpgradeType.WEAPON)
                    this.UpdateDPS(this._database.GetDPS());
            };
        }

        public void SetStage(int stage)
        {
            this._stageLabel.text = string.Format(ScriptLocalization.HUD.STAGE, stage);
        }

        internal void UpdateRemainingTime(float remainingTime)
        {
            this._textRemainingTime.text = remainingTime.ToTimeNotation();
        }

        internal void DisplayRecoveringState()
        {
            this._sliderLevelBar.value = 0;
            this._textRemainingTime.text = ScriptLocalization.HUD.RECOVERING_STATE;
        }

        internal void UpdateWave(EnemyWaveState state)
        {
            if (state.IsBossActive)
            {
                this._levelBarFill.color = Color.red;
            }
            else
            {
                this._levelBarFill.color = Color.yellow;
            }
            this._sliderLevelBar.maxValue = state.Goal;
            this._sliderLevelBar.value = state.Progress;
        }

        internal void DisplayPlayerCollision()
        {
            CameraHandler.Instance.MainCamera.DOKill();
            CameraHandler.Instance.MainCamera.DOColor(Color.clear, 1f);
        }

        internal void UpdateHealth(float health, float maxHealth)
        {
            this._sliderHealthBar.maxValue = maxHealth;
            this._sliderHealthBar.value = health;
            this._textHealthBar.text = string.Format(ScriptLocalization.HUD.HEALTH_DISPLAY,
                                                     health.ToAlphabeticNotation(),
                                                     maxHealth.ToAlphabeticNotation());

            this._textPercentHealth.text = $"{100f * health / maxHealth:0}%";

            this._textPercentHealth.alpha = 1f;

            this._tweenPercentHealth?.Kill();
            this._tweenPercentHealth = this._textPercentHealth.DOFade(0, HEALTH_PERCENT_DISPLAY_DURATION);
        }

        private void UpdateDPS(float newValue)
        {
            this._textDPS.text = newValue.ToAlphabeticNotation();
        }
    }
}