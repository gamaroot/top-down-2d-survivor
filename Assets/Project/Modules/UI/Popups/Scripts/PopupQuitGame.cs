using UnityEngine;

namespace Game
{
    internal class PopupQuitGame : Popup
    {
        public void OnDenyButtonClick() => Application.Quit();
    }
}