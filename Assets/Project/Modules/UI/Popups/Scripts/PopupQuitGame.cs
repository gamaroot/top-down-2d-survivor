using ScreenNavigation;

namespace Game
{
    internal class PopupQuitGame : Popup
    {
        public void OnDenyButtonClick()
        {
            SceneNavigator.Instance.UnloadSceneAsync(SceneID.GAME);
            SceneNavigator.Instance.LoadAdditiveSceneAsync(SceneID.STAGE_SELECTION);

            base.Hide();
        }
    }
}