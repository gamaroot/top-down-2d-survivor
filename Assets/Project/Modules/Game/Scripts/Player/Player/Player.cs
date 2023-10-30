using Database;
using DG.Tweening;
using System;
using UnityEngine;

namespace Game
{
    internal class Player : Body, IPlayerInfo
    {
        [Range(1f, 30f)]
        [SerializeField] private float _recoverDuration = 10f;

        [Header("Components")]
        [SerializeField] private HUD _hud;
        [SerializeField] private Animator _radar;
        [SerializeField] private ParticleSystem[] _particles;

        internal static IPlayerInfo Info { get; private set; }

        private void OnValidate()
        {
            if (this._particles == null)
                this._particles = base.GetComponentsInChildren<ParticleSystem>();
        }

        private void Awake()
        {
            Info = this;
            base.indestructible = true;
        }

        internal void Setup(float maxHealth)
        {
            UpgradeItemData.OnLevelChange += (data) =>
            {
                if (data.Type == UpgradeType.MAX_HEALTH)
                    base.MaxHealth = ((StatsData)data).GetValue();
            };
            base.Health = maxHealth;
            base.OnHealthUpdated = this._hud.UpdateHealth;
            base.MaxHealth = maxHealth;
        }

        public Vector3 GetPosition()
        {
            return base.transform.position;
        }

        internal void SetRecoverStateActive(Action onComplete)
        {
            this._radar.SetBool(AnimationParams.VISIBLE, false);

            DOTween.To(() => 0, x => base.Health = x, base.MaxHealth, this._recoverDuration)
                   .OnComplete(() =>
                   {
                       this._radar.SetBool(AnimationParams.VISIBLE, true);

                       onComplete.Invoke();
                   });
        }
    }
}