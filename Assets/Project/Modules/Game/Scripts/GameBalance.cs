using System;

namespace Game
{
    internal class GameBalance
    {
        public static float Calculate(float value, float level) => Math.Max(value, value * (float)Math.Pow(level, 1.01));

        public static float CalculatePrice(float value, float level) => Math.Max(value, value * (float)Math.Pow(level, 1.2));

        public static float CalculateEnemyHealth(float value, float level) => Math.Max(value, value * (float)Math.Pow(level, 2.5f)) * 2.5f;
    }
}