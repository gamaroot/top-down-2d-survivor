using UnityEngine;

namespace Game
{
    internal class PopupManager : MonoBehaviour
    {
        [SerializeField] private Popup _gameRating, _quitApplication, _quitGame, _stageCleared;

        internal void ForceHide() => base.transform.GetChild(0).GetComponent<Popup>().Hide();

        internal Popup ShowStageCleared() => this._stageCleared.Show();
        internal Popup ShowGameRating() => this._gameRating.Show();
        internal void ShowQuitApplication() => this._quitApplication.Show();
        internal void ShowQuitGame() => this._quitGame.Show();
    }
}