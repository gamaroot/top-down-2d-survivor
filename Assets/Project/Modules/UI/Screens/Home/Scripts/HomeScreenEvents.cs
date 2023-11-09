using ScreenNavigation;
using UnityEngine;
using Utils;

namespace Game
{
    internal class HomeScreenEvents : MonoBehaviour
    {
        private PopupManager _popupManager;

        private void Awake()
        {
            this._popupManager = new CrossSceneReference().GetObjectByType<PopupManager>();
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_ANDROID
            if (!Popup.IsOpen && Input.GetKeyDown(KeyCode.Escape))
                this._popupManager.ShowQuitApplication();
#endif
        }

        // Called through inspector
        public void OnPlayButtonClick()
        {
            if (SceneNavigator.Instance.IsSceneOpened(SceneID.GENERAL_SETTINGS))
            {
                SceneNavigator.Instance.UnloadSceneAsync(SceneID.GENERAL_SETTINGS);
            }
            SceneNavigator.Instance.UnloadSceneAsync(SceneID.HOME);
            SceneNavigator.Instance.LoadAdditiveSceneAsync(SceneID.STAGE_SELECTION);
        }
    }
}