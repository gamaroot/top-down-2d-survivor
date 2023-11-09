using ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Button), typeof(Animator))]
    internal class SettingsButton : MonoBehaviour
    {
        [SerializeField] private SceneID _sceneID;
        [SerializeField] private Animator _animator;

        private void OnValidate()
        {
            if (this._animator == null)
                this._animator = base.GetComponent<Animator>();
        }

        // Called on Inspector
        public void ToggleVisibility()
        {
            bool visibility = !this._animator.GetBool(AnimationParams.VISIBLE);
            this._animator.SetBool(AnimationParams.VISIBLE, visibility);

            if (visibility)
                SceneNavigator.Instance.LoadAdditiveSceneAsync(this._sceneID);
            else
                SceneNavigator.Instance.UnloadSceneAsync(this._sceneID);
        }
    }
}
