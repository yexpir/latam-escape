using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class ArrayExtensions
    {
        public static void Write(this Array array, Array content, int startIndex = 0)
        {
            /*if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (content == null)
                throw new ArgumentNullException(nameof(content));
            if (startIndex < 0 || startIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Start index must be within the bounds of the target array.");
            if (startIndex + content.Length > array.Length)
                throw new ArgumentException("The source array is too large to fit starting at the specified index.");*/

            Array.Copy(content, 0, array, startIndex, content.Length);
        }
        
        public static T Mirror<T>(this T[] array, int i)
        {
            return array[array.Length - (i + 1)];
        }

        public static string ToPrint<T>(this IEnumerable<T> list)
        {
            return list.Aggregate("", (current, e) => current + $"{e}\n");
        }
        
        public static string ToPrintInLine<T>(this IEnumerable<T> list)
        {
            return list.Aggregate("", (current, e) => current + $"{e} ");
        }
        
        static string ToPrintBinary(this IEnumerable<int> list)
        {
            return list.Aggregate("", (current, t) => current + t.BitToString() + "\n");
        }
    }
}