using ScreenNavigation;

namespace Game
{
    internal class PopupQuitGame : Popup
    {
        public void OnDenyButtonClick()
        {
            if (SceneNavigator.Instance.IsSceneOpened(SceneID.INGAME_SETTINGS))
            {
                SceneNavigator.Instance.UnloadSceneAsync(SceneID.INGAME_SETTINGS);
            }
            SceneNavigator.Instance.UnloadSceneAsync(SceneID.GAME);
            SceneNavigator.Instance.LoadAdditiveSceneAsync(SceneID.STAGE_SELECTION);

            base.Hide();
        }
    }
}