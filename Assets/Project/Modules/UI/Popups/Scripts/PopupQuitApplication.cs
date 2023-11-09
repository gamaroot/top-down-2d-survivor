using UnityEngine;

namespace Game
{
    internal class PopupQuitApplication : Popup
    {
        public void OnDenyButtonClick()
        {
            Application.Quit();
        }
    }
}