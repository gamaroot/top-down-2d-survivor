using Database;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils;

namespace Game
{
    internal class UpgradePanelItem : MonoBehaviour
    {
        private const float FADE_DURATION = 0.5f;
        private const float SCALE_DURATION = 0.5f;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _textValue, _textPrice, _textLevel;
        [SerializeField] private Button _buttonBuy;
        [SerializeField] private Image _highlight;

        internal string Value {
            set
            {
                if (this._textValue != null)
                    this._textValue.text = value;
            }
        }
        internal string Price { get => this._textPrice.text; set { this._textPrice.text = value; } }

        Sequence _sequence;

        internal virtual void Load(UpgradeItemData data) => this.UpdateValues(data);

        internal void SetOnButtonBuyClick(UnityAction onClick)
        {
            this._buttonBuy.onClick.AddListener(onClick);
        }

        internal void OnUpgrade(bool success, UpgradeItemData data)
        {
            this.UpdateValues(data);

            this._highlight.gameObject.SetActive(true);

            Vector2 resetScale = this._highlight.transform.localScale;
            resetScale.x = 0;
            this._highlight.transform.localScale = resetScale;
            this._highlight.color = success ? Color.green : Color.red;

            this._sequence?.Kill();
            this._sequence = DOTween.Sequence();
            this._sequence.Join(this._highlight.DOFade(0, FADE_DURATION));
            this._sequence.Join(this._highlight.transform.DOScaleX(1f, SCALE_DURATION));
            this._sequence.OnComplete(() => this._highlight.gameObject.SetActive(false));
        }

        internal virtual void UpdateValues(UpgradeItemData data)
        {
            if (data.Level > 0)
                this._textLevel.text = $"{ScriptLocalization.UpgradePanel.LEVEL} {data.Level.ToAlphabeticNotation()}";

            this.Value = $"{data.GetValue().ToAlphabeticNotation()} <size=50%>{data.ValueSuffix}</size>";
            this.Price = $"${data.GetCost().ToAlphabeticNotation()}";
        }
    }
}