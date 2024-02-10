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
            if(min == 0 && max == 270)
            {
                if (x == 0f) x = 360f;
                min = max;
                max = 360f;
            }
            return x >= min && x <= max;
        }

        public static bool IsAngleInRange(float angleToCheck, float angle1, float angle2)
        {
            if (angle1 <= angle2)
                return angleToCheck >= angle1 && angleToCheck <= angle2;
            return angleToCheck >= angle1 || angleToCheck <= angle2;
        }
        
        public static float NormalizeAngle(float angle)
        {
            angle %= 360;
            if (angle < 0)
                angle += 360;
            return angle;
        }
        
        public static float RoundTo(float toRound, float roundTo) => Mathf.Round(toRound / roundTo) * roundTo;
    }
}