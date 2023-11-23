using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    internal class Popup : MonoBehaviour
    {
        [field: SerializeField] internal Animator Animator { get; private set; }

        [SerializeField] private bool _dimissable;
        [SerializeField] private Button _buttonConfirm, _buttonDeny;
        [SerializeField] private TextMeshProUGUI _textTitle, _textMessage, _textButtonDeny, _textButtonConfirm;

        [field: SerializeField] internal UnityAction OnShowListener, OnHideListener;

        internal static bool IsOpen { get; private set; }

        private void OnValidate()
        {
            if (this.Animator == null)
                this.Animator = base.GetComponent<Animator>();
        }

        private void OnEnable()
        {
            this.Animator.SetBool(AnimationParams.VISIBLE, true);
        }

        protected virtual void Update()
        {
    #if UNITY_EDITOR || UNITY_WEBGL || UNITY_ANDROID
            if (this._dimissable && Input.GetKeyDown(KeyCode.Escape))
                this.Hide();
    #endif
        }

        internal string Title
        {
            get { return this._textTitle.text; }
            set { this._textTitle.text = value; }
        }

        internal string Message
        {
            get { return this._textMessage.text; }
            set { this._textMessage.text = value; }
        }

        internal string ButtonDenyText
        {
            get { return this._textButtonDeny.text; }
            set { this._textButtonDeny.text = value; }
        }

        internal string ButtonConfirmText
        {
            get { return this._textButtonConfirm.text; }
            set { this._textButtonConfirm.text = value; }
        }

        public Popup Show()
        {
            IsOpen = true;
            base.gameObject.SetActive(true);

            return this;
        }

        public void Hide()
        {
            this._buttonDeny?.onClick.RemoveAllListeners();
            this._buttonConfirm?.onClick.RemoveAllListeners();

            IsOpen = false;
            this.Animator.SetBool(AnimationParams.VISIBLE, false);
        }

        public Popup AddConfirmButtonClick(UnityAction onClick)
        {
            this._buttonConfirm.onClick.AddListener(onClick);
            return this;
        }

        public Popup AddDenyButtonClick(UnityAction onClick)
        {
            this._buttonDeny.onClick.AddListener(onClick);
            return this;
        }

        private void OnDialogShow()
        {
            this.OnShowListener?.Invoke();
        }

        private void OnDialogHide()
        {
            this.OnHideListener?.Invoke();
            base.gameObject.SetActive(false);
        }
    }
}