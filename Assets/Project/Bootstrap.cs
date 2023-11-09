using DG.Tweening;
using ScreenNavigation;
using UnityEngine;
using Utils;

namespace Game
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] private SceneID _firstScene;

        private void Awake()
        {
            DOTween.Init(this);
            DOTween.SetTweensCapacity(500, 50);

            Application.targetFrameRate = 60;

            Wallet.Load();
            Reward.Load();
            CameraHandler.Load();
            Statistics.Load();
        }

        private void Start()
        {
            SceneNavigator.Initialize();
            SceneNavigator.Instance.LoadAdditiveSceneAsync(this._firstScene);

            Destroy(base.gameObject);
        }
    }
}