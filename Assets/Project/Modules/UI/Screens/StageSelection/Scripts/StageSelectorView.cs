using Database;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    internal class StageSelectorView : MonoBehaviour
    {
        [SerializeField] private Image _display;
        [SerializeField] private Image _frame;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Button _button;

        public void Setup(StageData data, UnityAction onClick)
        {
            this._label.text = data.Label;
            this._display.sprite = data.Sprite;
            this._frame.color = data.FrameColor;

            this._button.onClick.AddListener(onClick);
        }
    }
}