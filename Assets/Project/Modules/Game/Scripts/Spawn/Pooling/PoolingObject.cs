using UnityEngine;

namespace Game
{
    internal class PoolingObject : MonoBehaviour
    {
        private Pool _pool;
        private float _autoDisableInSeconds = -1f;

        private void OnEnable()
        {
            base.transform.SetParent(this._pool.GetParent());

            if (this._autoDisableInSeconds > 0)
                base.Invoke("Disable", this._autoDisableInSeconds);
        }

        private void OnDisable()
        {
            this.OnPoolingObjectDisable();
        }

        internal void SetResourcePool(Pool resourcePool)
        {
            this._pool = resourcePool;
        }

        internal void Disable()
        {
            if (!base.gameObject.activeSelf)
                return;

            base.gameObject.SetActive(false);
        }

        internal void ScheduleAutoDisableInSeconds(float time)
        {
            this._autoDisableInSeconds = time;
        }

        private void OnPoolingObjectDisable()
        {
            this._pool?.SendBackToThePool(base.gameObject);
        }
    }
}