using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    internal class Popup : MonoBehaviour
    {
        [field: SerializeField] internal Animator Animator { get; private set; }

        [SerializeField] private bool _dimissable;
        [SerializeField] private TextMeshProUGUI _textTitle, _textMessage;

        internal UnityAction OnShowListener, OnHideListener;

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

        public void Show()
        {
            IsOpen = true;
            base.gameObject.SetActive(true);
        }

        public void Hide()
        {
            IsOpen = false;
            this.Animator.SetBool(AnimationParams.VISIBLE, false);
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