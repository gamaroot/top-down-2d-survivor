using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Enemy : Body
    {
        internal float Speed = 1f;

        private Vector2 _moveDirection;
        private float _rotationSpeed;

        private void Start()
        {
            this._rotationSpeed = UnityEngine.Random.Range(100f, 500f);
        }

        private void FixedUpdate()
        {
            base.Rigidbody.rotation += _rotationSpeed * Time.deltaTime;
            base.Rigidbody.velocity = this._moveDirection;
        }

        private void Update()
        {
            this._moveDirection = this.Speed * (Player.Info.GetPosition() - base.transform.position).normalized;
        }

        internal void TurnIntoBoss()
        {
            base.Health *= 100f;
            base.MaxHealth = base.Health;
            base.transform.localScale *= 4f;
            base.Damage = float.MaxValue;
        }

        internal override void OnHealthZero(DamagerObjectType type)
        {
            base.OnHealthZero(type);
            base.OnDestroy = (_) => { };
            base.OnHealthUpdated = (_, _) => { };
        }
    }
}