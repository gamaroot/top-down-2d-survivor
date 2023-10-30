using UnityEngine;

namespace Game
{
    internal class PopupManager : MonoBehaviour
    {
        [SerializeField] private Popup _pushPermission, _gameRating, _quitGame, _moreCoins, _doublePower, _speedUp;

        internal void ForceHide() => base.transform.GetChild(0).GetComponent<Popup>().Hide();

        internal void ShowPushPermission() => this._pushPermission.Show();
        internal void ShowGameRating() => this._gameRating.Show();
        internal void ShowQuitGame() => this._quitGame.Show();
        internal void ShowMoreCoins() => this._moreCoins.Show();
        internal void ShowDoublePower() => this._doublePower.Show();
        internal void ShowSpeedUp() => this._speedUp.Show();
    }
}