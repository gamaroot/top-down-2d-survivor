using UnityEngine;

namespace Game
{
    internal class CoachMark : MonoBehaviour
    {
        [SerializeField] protected Animator _animator;

        private void OnValidate()
        {
            if ( _animator == null)
                this._animator = base.GetComponent<Animator>();
        }

        public virtual void OnAnimationEnd() => Destroy(base.gameObject);
    }
}
