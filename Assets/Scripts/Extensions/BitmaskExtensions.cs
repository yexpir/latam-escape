using System;

namespace Extensions
{
    public static class BitmaskExtensions
    {
        public static string BitToString(this int bitmask)
        {
            return Convert.ToString(bitmask, 2).PadLeft(8, '0');
        }
    }
}