using Database;
using DG.Tweening;
using ScreenNavigation;
using UnityEngine;
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

        private PopupManager _popupManager;
        private CoachMarkManager _coachMark;

        private void Awake()
        {
            var crossSceneReference = new CrossSceneReference();
            this._popupManager = crossSceneReference.GetObjectByType<PopupManager>();
            this._coachMark = crossSceneReference.GetObjectByType<CoachMarkManager>();

            SceneNavigator.Instance.AddListenerOnScreenStateChange(this.OnSceneStateChange);

            this._player.Setup(this._itemDatabase.GetStatsValue(UpgradeType.MAX_HEALTH));
            this._player.OnDestroy = (_) => this.OnMatchEnd();

            this._matchTimeHandler.OnTimeout = this.OnMatchEnd;
            this._matchTimeHandler.OnUpdate = this._hud.UpdateRemainingTime;

            this._waveController.StateUpdateListener = this._hud.UpdateWave;
            this._waveController.OnKillListener = Reward.Instance.OnEnemyDefeat;

            this.SetupCoachMarks();
        }

        private void Start()
        {
            var data = (StageData)SceneNavigator.Instance.GetSceneParams(SceneID.GAME);
            CameraHandler.Instance.MainCamera.DOColor(data.FrameColor, 2f);

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
            this._matchTimeHandler.ResetTimer();
            this._waveController.StartSpawn();
        }

        private void OnMatchEnd()
        {
            this._waveController.ResetLevel();

            Statistics.Instance.IncrementMatches();
            Statistics.Instance.ResetCurrentWave();

            this._hud.OnReset();

            this._player.SetRecoverStateActive(this.OnMatchStart);

            if (PopupGameRating.NeedToShow())
                this._popupManager.ShowGameRating();
            else
                this.CheckCoachMarks();
        }

        private void SetupCoachMarks()
        {
            var coachMarks = new CoachMarkType[]
            {
                CoachMarkType.DESTROY_ENEMIES,
                CoachMarkType.IMPROVE_YOUR_WEAPON,
                CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN
            };
            if (this._itemDatabase.GetDPS() >= 15)
            {
                this._coachMark.SetAsShown(coachMarks);
            }
            else if (this._coachMark.NeedToShow(coachMarks))
            {
                Wallet.Instance.OnChange += this.CheckCoachMarkWhenGetCoins;
            }
        }

        private void CheckCoachMarkWhenGetCoins(float _, float currCoins)
        {
            if (!this._coachMark.isActiveAndEnabled)
                return;

            if (currCoins >= 3 &&
                this._coachMark.NeedToShow(CoachMarkType.DESTROY_ENEMIES))
            {
                this._coachMark.Show(CoachMarkType.DESTROY_ENEMIES);
            }
            else if(currCoins >= 10 &&
                    this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON))
            {
                this._coachMark.Show(CoachMarkType.IMPROVE_YOUR_WEAPON);
            }
            else if (currCoins >= 22 &&
                     this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN))
            {
                this._coachMark.Show(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN);
                Wallet.Instance.OnChange -= this.CheckCoachMarkWhenGetCoins;
            }
        }

        private void CheckCoachMarks()
        {
            if (this._itemDatabase.GetDPS() <= 15 &&
                this._coachMark.NeedToShow(CoachMarkType.BUY_STRONGER_WEAPON) &&
               !this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON) &&
               !this._coachMark.NeedToShow(CoachMarkType.IMPROVE_YOUR_WEAPON_AGAIN))
            {
                this._coachMark.Show(CoachMarkType.BUY_STRONGER_WEAPON);
            }
        }

        private void OnSceneStateChange(SceneID sceneId, SceneState newState)
        {
            if (sceneId == SceneID.GAME && newState == SceneState.ANIMATING_HIDE)
            {
                SceneNavigator.Instance.RemoveListenerOnScreenStateChange(this.OnSceneStateChange);

                CameraHandler.Instance.MainCamera.DOColor(Color.black, 2f);
                this._waveController.ResetLevel();
            }
        }
    }
}