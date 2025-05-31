using System.Linq;
using UnityEngine;
using Utils;

namespace Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        ///   <para>Maps Z to Y and converts to Vector2 (1, 0, 1) -> (1, 1)</para>
        /// </summary>
        public static Vector2 To2(this Vector3 vector3)// (1, 0, 1) -> (1, 1)
        {
            Vector2 vector2 = vector3;
            vector2.y = vector3.z;
            return vector2;
        }
        
        public static Vector3 Flatten(this Vector3 v)
        {
            v.y = 0;
            return v;
        }

        public static Vector3 Mult(this Vector3 v, Vector3 m)
        {
            return new (v.x*m.x, v.y*m.y, v.z*m.z);
        }

        public static Vector3 OnlyX(this Vector3 v)
        {
            v.y = 0.0f;
            v.z = 0.0f;
            return v;
        }
        public static Vector3 OnlyY(this Vector3 v)
        {
            v.x = 0.0f;
            v.z = 0.0f;
            return v;
        }
        public static Vector3 OnlyZ(this Vector3 v)
        {
            v.x = 0.0f;
            v.y = 0.0f;
            return v;
        }

        public static Vector3 Abs(this Vector3 v)
        {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            v.z = Mathf.Abs(v.z);
            return v;
        }

        public static void SetFlat(ref this Vector3 vector, float newX, float newZ)
        {
            vector.x = newX;
            vector.z = newZ;
        }

        public static Vector3 SetX(this Vector3 vector, float newX)
        {
            vector.x = newX;
            return vector;
        }

        public static Vector3 SetY(this Vector3 vector, float newY)
        {
            vector.y = newY;
            return vector;
        }

        public static Vector3 SetZ(this Vector3 vector, float newZ)
        {
            vector.z = newZ;
            return vector;
        }

        

        public static float Max(this Vector3 vector) => Mathf.Abs(vector.x) > Mathf.Abs(vector.z) ? vector.x : vector.z;
        
        public static float GetLargestAxisValue(this Vector3 vector)
        {
            if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y) && Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
                return vector.x;
            return Mathf.Abs(vector.y) > Mathf.Abs(vector.z) ? vector.y : vector.z;
        }

        public static Vector3 Project(this Vector3 vector, Vector3 projector)
        {
            if (projector.x != 0.0f)
                vector.x = projector.x;
            if (projector.y != 0.0f)
                vector.y = projector.y;
            if (projector.z != 0.0f)
                vector.z = projector.z;
            return vector;
        }

        public static Vector3 ProjectWithSelector(this Vector3 vector, Vector3 projector, Vector3 selector)
        {
            if (selector.x != 0.0f)
                vector.x = projector.x;
            if (selector.y != 0.0f)
                vector.y = projector.y;
            if (selector.z != 0.0f)
                vector.z = projector.z;
            return vector;
        }
        
        public static Vector3 ProjectValueWithSelector(this Vector3 vector, float value, Vector3 selector)
        {
            selector = selector.Round();
            if (selector.x != 0)
                vector.x = value;
            if (selector.y != 0)
                vector.y = value;
            if (selector.z != 0)
                vector.z = value;
            return vector;
        }

        public static Vector3 Round(this Vector3 vector, float roundUnit = 1)
        {
            vector.x = Mathf.RoundToInt(vector.x / roundUnit) * roundUnit;
            vector.y = Mathf.RoundToInt(vector.y / roundUnit) * roundUnit;
            vector.z = Mathf.RoundToInt(vector.z / roundUnit) * roundUnit;
            return vector;
        }
        public static Vector3 Floor(this Vector3 vector, float roundUnit = 1)
        {
            vector.x = Mathf.FloorToInt(vector.x / roundUnit) * roundUnit;
            vector.y = Mathf.FloorToInt(vector.y / roundUnit) * roundUnit;
            vector.z = Mathf.FloorToInt(vector.z / roundUnit) * roundUnit;
            return vector;
        }
        public static Vector3 Ceil(this Vector3 vector, float roundUnit = 1)
        {
            vector.x = Mathf.CeilToInt(vector.x / roundUnit) * roundUnit;
            vector.y = Mathf.CeilToInt(vector.y / roundUnit) * roundUnit;
            vector.z = Mathf.CeilToInt(vector.z / roundUnit) * roundUnit;
            return vector;
        }

        public static float Select(this Vector3 vector, Vector3 selector) => vector.Mult(selector.Abs().Round()).Max();
        
        public static float GetStartAngle(this Vector3 from, Vector3 to) => M.Mod(-Vector3.SignedAngle(Vector3.right, (to - from).Flatten().normalized, Vector3.up),360f);

        public static string ToPrintRaw(this Vector3 vector)
        {
            return $"({vector.x:R}, {vector.y:R}, {vector.z:R})";
        }
    }
}