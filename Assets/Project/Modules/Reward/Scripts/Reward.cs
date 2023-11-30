using System;

namespace Game
{
    internal class Reward
    {
        internal static Reward Instance { get; private set; }
        internal static void Load() => Instance = new Reward();

        internal int RewardedVideoCoins { get; private set; }

        internal void OnEnemyDefeat(bool isBoss, DamagerObjectType killedBy)
        {
            float reward = Math.Max(1, Statistics.Instance.CurrentWave / 1.5f);
            if (isBoss)
                reward *= 20f;

            Wallet.Instance.AddCoins(reward);
        }
    }
}