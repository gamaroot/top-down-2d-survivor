using System;
using System.Collections.Generic;

namespace Utils
{
    // Source: https://gram.gs/gramlog/formatting-big-numbers-aa-notation/
    internal static class NumberFormatter
    {
        private static readonly int CHAR_A = Convert.ToInt32('a');

        private static readonly Dictionary<int, string> UNITS = new()
        {
            {0, ""},
            {1, "K"},
            {2, "M"},
            {3, "B"},
            {4, "T"}
        };

        internal static string ToAlphabeticNotation(this long value) => ToAlphabeticNotation((float)value);

        internal static string ToAlphabeticNotation(this double value) => ToAlphabeticNotation((float)value);

        internal static string ToAlphabeticNotation(this int value) => ToAlphabeticNotation((float)value);

        internal static string ToAlphabeticNotation(this float value)
        {
            if (value < 1f) return "0";

            int expNumber = (int)Math.Log(value, 1000f);
            float baseNumber = value / (float)Math.Pow(1000f, expNumber);
            
            string unit;
            if (expNumber < UNITS.Count)
            {
                unit = UNITS[expNumber];
            }
            else
            {
                int unitInt = expNumber - UNITS.Count;
                int secondUnit = unitInt % 26;
                int firstUnit = unitInt / 26;
                unit = $"{Convert.ToChar(firstUnit + CHAR_A)}{Convert.ToChar(secondUnit + CHAR_A)}";
            }

            return $"{Math.Floor(baseNumber * 100f) / 100f:0}{unit}";
        }

        internal static string ToTimeNotation(this float value)
        {
            if (value < 1f) return "0:00";

            var timeSpan = TimeSpan.FromSeconds(value);

            return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
