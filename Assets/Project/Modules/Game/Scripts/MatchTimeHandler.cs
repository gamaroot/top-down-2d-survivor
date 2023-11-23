using System;
using UnityEngine;

namespace Game
{
    internal class MatchTimeHandler : MonoBehaviour
    {
        [SerializeField] private float _totalDuration = 300f;

        internal Action<float> OnUpdate { private get; set; }
        internal Action OnTimeout { private get; set; }

        private bool _isStopped;
        private float _remainingTime;

        private void Update()
        {
            if (this._isStopped) return;

            this._remainingTime -= Time.deltaTime;
            this.OnUpdate(this._remainingTime);
        }

        internal void ResetTimer()
        {
            _isStopped = false;
            this._remainingTime = this._totalDuration;
        }

        internal void StopTimer()
        {
            _isStopped = true;
        }
    }
}
