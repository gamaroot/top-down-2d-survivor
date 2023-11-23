using UnityEngine;

namespace DebugMode
{
    internal class DebugScreen : MonoBehaviour
    {
#if !DEBUG_MODE
        private void OnValidate()
        {
            base.gameObject.SetActive(false);
        }

        private void Awake()
        {
            Destroy(base.gameObject);
        }
#else
        private void OnValidate()
        {
            base.gameObject.SetActive(true);
        }
#endif
    }
}