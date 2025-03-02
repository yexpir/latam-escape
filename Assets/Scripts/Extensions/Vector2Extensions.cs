using UnityEngine;

namespace Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        ///   <para>Converts to Vector3 and maps Y to Z (1, 1) -> (1, 0, 1)</para>
        /// </summary>
        public static Vector3 To3(this Vector2 vector2)// (1, 1) -> (1, 0, 1)
        {
            Vector3 vector3 = vector2;
            vector3.z = vector2.y;
            vector3.y = 0;
            return vector3;
        }
        
        public static void SetX(this Vector2 vector, float newX) => vector.x = newX;
        public static void SetY(this Vector2 vector, float newY) => vector.y = newY;
    }
}