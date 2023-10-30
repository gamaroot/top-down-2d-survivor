using UnityEngine;

namespace Game
{
    internal class CoachMarkWithInput : CoachMark
    {
        private float _previousTimeScale;
        private bool hasAnimationStartEnded;

        private void OnEnable()
        {
            this._previousTimeScale = Time.timeScale;
            if (this._previousTimeScale == 0)
                this._previousTimeScale = 1f;

            Time.timeScale = 0;
        }

        private void Update()
        {
            if (this.hasAnimationStartEnded &&
#if UNITY_EDITOR
                Input.GetMouseButtonDown(0))
#else
                Input.touchCount > 0 &&
                Input.GetTouch(0).phase == TouchPhase.Ended)
#endif
            {
                base._animator.SetBool(AnimationParams.VISIBLE, false);
            }
        }

        public void OnAnimationStart()
        {
            this.hasAnimationStartEnded = true;
        }

        public override void OnAnimationEnd()
        {
            Time.timeScale = this._previousTimeScale;
            base.OnAnimationEnd();
        }
    }
}
