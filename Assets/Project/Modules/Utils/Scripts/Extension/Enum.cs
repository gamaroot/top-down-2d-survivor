using System;

namespace Utils
{
    // T must be a value type and implement the IConvertiable interface.
    // this is to prevent users from passing
    // incorrect formats and to maintain secure code.
    internal class Enum<T> where T : struct, IConvertible
    {

        // returns the number of elements in the enumeration.
        internal static int Length()
        {
            return Enum.GetValues(typeof(T)).Length;
            // the length of array is returned
        }
    }
}