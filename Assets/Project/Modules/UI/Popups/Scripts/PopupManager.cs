using UnityEngine;

namespace Game
{
    internal class PopupManager : MonoBehaviour
    {
        [SerializeField] private Popup _gameRating, _quitApplication, _quitGame;

        internal void ForceHide() => base.transform.GetChild(0).GetComponent<Popup>().Hide();

        internal void ShowGameRating() => this._gameRating.Show();
        internal void ShowQuitApplication() => this._quitApplication.Show();
        internal void ShowQuitGame() => this._quitGame.Show();
    }
}