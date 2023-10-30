using UnityEngine;

namespace Game
{
    internal class ToastHandler : MonoBehaviour
    {
        [SerializeField] private Toast _prefab;

        private Toast _toast;

        internal void Show(string message)
        {
            if (this._toast)
                Destroy(this._toast.gameObject);

            this._toast = Instantiate(this._prefab);
            this._toast.Message.text = message;
            this._toast.transform.SetParent(base.transform, false);
        }
    }
}