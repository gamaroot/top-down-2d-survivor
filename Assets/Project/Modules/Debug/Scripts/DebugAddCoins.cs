using Game;
using UnityEngine;

namespace DebugMode
{
    internal class DebugAddCoins : MonoBehaviour
    {
#if DEBUG_MODE
        internal void AddCoins()
        {
            Wallet.Instance.AddCoins(999999);
        }
#else
        private void Awake()
        {
            Destroy(base.gameObject);
        }
#endif
    }
}