using System;
using UnityEngine;

namespace Game {
    internal class Wallet
    {
        internal static Wallet Instance { get; private set; }

        internal Action<float, float> OnChange;

        internal float Coins
        {
            get => this._coins;
            private set
            {
                this.OnChange.Invoke(this._coins, value);

                this._coins = value;

                PlayerPrefs.SetFloat(PlayerPrefsKeys.LOCAL_COINS_KEY, this._coins);
                PlayerPrefs.Save();
            }
        }
        private float _coins;

        internal static void Load()
        {
            Instance = new Wallet
            {
                _coins = PlayerPrefs.GetFloat(PlayerPrefsKeys.LOCAL_COINS_KEY)
            };
        }

        internal void AddCoins(float amount)
        {
            this.Coins += amount;
        }

        internal void SpendCoins(float amount)
        {
            this.Coins -= amount;
        }
    }
}