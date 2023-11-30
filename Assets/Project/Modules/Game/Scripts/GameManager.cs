using Database;
using DG.Tweening;
using ScreenNavigation;
using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Game
{
    internal class GameManager : MonoBehaviour
    {
        [SerializeField] private UpgradeItemDatabase _itemDatabase;

        [Header("Components")]
        [SerializeField] private HUD _hud;
        [SerializeField] private Player _player;
        [SerializeField] private EnemyWaveController _waveController;
        [SerializeField] private MatchTimeHandler _matchTimeHandler;
        [SerializeField] private StageHandler _stageHandler;

        [Header("Events")]
        [SerializeField] private OnPlayerLoseEvent _onPlayerLose;

        private PopupManager _popupManager;

        private void Awake()
        {
            this._popupManager = new CrossSceneReference().GetObjectByType<PopupManager>();

            SceneNavigator.Instance.AddListenerOnScreenStateChange(this.OnSceneStateChange);

            this._player.Setup(this._itemDatabase.GetStatsValue(UpgradeType.MAX_HEALTH));
            this._player.OnDestroy = (_) => this.OnPlayerLose();

            this._matchTimeHandler.OnTimeout = this.OnPlayerLose;
            this._matchTimeHandler.OnUpdate = this._hud.UpdateRemainingTime;

            this._waveController.Setup(this._player);
            this._waveController.StateUpdateListener = this._hud.UpdateWave;
            this._waveController.OnKillListener = this.OnEnemyKilled;
        }

        private void Start()
        {
            var data = (StageData)SceneNavigator.Instance.GetSceneParams(SceneID.GAME);
            Statistics.Instance.CurrentWave = data.Level;

            data.PlayerInfo = this._player;

            this._stageHandler.Setup(data);

            this.OnMatchStart();
        }

        private void Update()
        {
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_ANDROID
            if (!Popup.IsOpen && Input.GetKeyDown(KeyCode.Escape))
                this._popupManager.ShowQuitGame();
#endif
        }

        private void OnMatchStart()
        {
            this._hud.SetStage(++Statistics.Instance.CurrentWave);
            this._matchTimeHandler.ResetTimer();
            this._waveController.StartSpawn();
        }

        private void OnPlayerWin()
        {
            this._waveController.ResetLevel();
            Statistics.Instance.ResetCurrentWave();

            if (PopupGameRating.NeedToShow())
                this._popupManager.ShowGameRating().OnHideListener = this.ShowStageClearedPopup;
            else
                this.ShowStageClearedPopup();
        }

        private void OnPlayerLose()
        {
            this._waveController.ResetLevel();

            Statistics.Instance.IncrementMatches();
            Statistics.Instance.ResetCurrentWave();

            this._matchTimeHandler.StopTimer();
            this._hud.DisplayRecoveringState();

            this._onPlayerLose.Invoke();

            this._player.SetRecoverStateActive(this.OnMatchStart);
        }

        private void OnEnemyKilled(bool isBoss, DamagerObjectType killedBy)
        {
            if (killedBy != DamagerObjectType.Bullet)
                return;

            Statistics.Instance.IncrementEnemiesDestroyed();
            Reward.Instance.OnEnemyDefeat(isBoss, killedBy);
            if (isBoss)
            {
                this.OnPlayerWin();
            }
        }

        private void OnSceneStateChange(SceneID sceneId, SceneState newState)
        {
            if (sceneId == SceneID.GAME && newState == SceneState.ANIMATING_HIDE)
            {
                SceneNavigator.Instance.RemoveListenerOnScreenStateChange(this.OnSceneStateChange);

                CameraHandler.Instance.MainCamera.DOColor(Color.black, 2f);
                this._waveController.ResetLevel();
                BulletPool.DisableAll();
            }
        }

        private void ShowStageClearedPopup()
        {
            this._popupManager.ShowStageCleared()
            .AddConfirmButtonClick(() =>
            {
                Statistics.Instance.IncrementWave();
                this.OnMatchStart();
            })
            .AddDenyButtonClick(this.OnMatchStart);
        }


        #region Event Listeners
        [Serializable]
        public class OnPlayerLoseEvent : UnityEvent { }
        #endregion
    }
}