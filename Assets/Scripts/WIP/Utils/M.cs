using UnityEngine;

namespace WIP.Utils
{
    public static class M
    {
        public static float Mod(float a, float b) => (a %= b) < 0 ? a+b : a;

        public static bool IsInRange(float x, float a, float b)
        {
            var min = Mathf.Min(a, b);
            var max = Mathf.Max(a, b);
            return x >= min && x <= max;
        }
        public static float RoundTo(float toRound, float roundTo) => Mathf.Round(toRound / roundTo) * roundTo;
    }
}