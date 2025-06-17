using UnityEngine;

namespace Extensions
{
    public static class FloatExtensions
    {
        public static float Abs(this float value)
        {
            return Mathf.Abs(value);
        }
    }
}