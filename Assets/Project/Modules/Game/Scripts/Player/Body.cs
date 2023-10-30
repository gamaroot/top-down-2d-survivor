using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class Body : DamagerObject
    {
        [field: SerializeField] internal Rigidbody2D Rigidbody { get; private set; }

        protected bool indestructible;

        internal float Health
        {
            get => this._health;
            set
            {
                if (this._health == value)
                    return;

                this._health = value > 0 ? value : 0;
                this.OnHealthUpdated.Invoke(this._health, this._maxHealth);
            }
        }
        private float _health = 100f;

        internal float MaxHealth
        {
            get => this._maxHealth;
            set
            {
                this._maxHealth = value;
                this.OnHealthUpdated.Invoke(this._health, this._maxHealth);
            }
        }
        private float _maxHealth = 100f;

        internal Action<DamagerObjectType> OnDestroy { private get; set; }
        internal Action<float, float> OnHealthUpdated = (_,_) => { };

        private void OnValidate()
        {
            if (this.Rigidbody == null)
                this.Rigidbody = base.GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision) => this.OnCollide(collision.gameObject);

        private void OnTriggerEnter2D(Collider2D collider) => this.OnCollide(collider.gameObject);

        internal virtual void OnHealthZero(DamagerObjectType type)
        {
            ExplosionPool.Spawn(base.transform.position);

            this.OnDestroy?.Invoke(type);
            if (!this.indestructible)
                base.gameObject.SetActive(false);
        }

        private void OnCollide(GameObject collisionObj)
        {
            if (this.Health <= 0) return;

            if (collisionObj.TryGetComponent(out DamagerObject obj) && obj.Type != base.Type)
            {
                this.Health -= obj.Type == DamagerObjectType.Player ? this.MaxHealth : obj.Damage;
                if (this.Health <= 0)
                    this.OnHealthZero(obj.Type);
            }
        }
    }
}