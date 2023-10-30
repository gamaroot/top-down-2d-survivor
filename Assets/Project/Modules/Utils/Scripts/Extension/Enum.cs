using System;

namespace Utils
{
    internal class Enum<T> where T : struct, IConvertible
    {
        internal static int Length()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}