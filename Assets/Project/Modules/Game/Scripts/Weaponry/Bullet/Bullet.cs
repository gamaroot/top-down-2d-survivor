using DG.Tweening;
using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CircleCollider2D))]
    internal class Bullet : DamagerObject
    {
        internal Action<Vector3> OnHit;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (base.gameObject.activeSelf)
            {
                DOTween.Kill(base.gameObject.transform);
                this.OnHit.Invoke(base.transform.position);

                base.gameObject.SetActive(false);
            }
        }
    }
}