using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        static Vector3 _vector;
        static Vector3 V(float x, float y, float z)
        {
            _vector.Set(x, y, z);
            return _vector;
        }
        public static void Move(this Transform t, Vector3 move) => t.position += move;
        public static void Move(this Transform t, float moveX, float moveY, float moveZ) => t.position += V(moveX, moveY, moveZ);
        public static void MoveX(this Transform t, float moveX) => t.Move(moveX, 0, 0);
        public static void MoveY(this Transform t, float moveY) => t.Move(0, moveY, 0);
        public static void MoveZ(this Transform t, float moveZ) => t.Move(0, 0, moveZ);
        
        public static void MoveXY(this Transform t, float moveX, float moveY) => t.Move(moveX, moveY, 0);
        public static void MoveXZ(this Transform t, float moveX, float moveZ) => t.Move(moveX, 0, moveZ);
        public static void MoveYZ(this Transform t, float moveY, float moveZ) => t.Move(0, moveY, moveZ);
        
        public static void Set(this Transform t, Vector3 pos) => t.position = pos;
        public static void Set(this Transform t, float x, float y, float z) => t.position += V(x, y, z);
        public static void SetX(this Transform t, float x) => t.position = V(x, t.position.y, t.position.z);
        public static void SetY(this Transform t, float y) => t.position = V(t.position.x, y, t.position.z);
        public static void SetZ(this Transform t, float z) => t.position = V(t.position.x, t.position.y, z);
        public static void SetXY(this Transform t, float x, float y) => t.position = V(x, y, t.position.z);
        public static void SetXZ(this Transform t, float x, float z) => t.position = V(x, t.position.y, z);
        public static void SetYZ(this Transform t, float y, float z) => t.position = V(t.position.x, y, z);
        
        public static void SetX(this Transform t, Vector3 newVector) => t.position = V(newVector.x, t.position.y, t.position.z);
        public static void SetY(this Transform t, Vector3 newVector) => t.position = V(t.position.x, newVector.y, t.position.z);
        public static void SetZ(this Transform t, Vector3 newVector) => t.position = V(t.position.x, t.position.y, newVector.z);
        public static void SetXY(this Transform t, Vector3 newVector) => t.position = V(newVector.x, newVector.y, t.position.z);
        public static void SetXZ(this Transform t, Vector3 newVector) => t.position = V(newVector.x, t.position.y, newVector.z);
        public static void SetYZ(this Transform t, Vector3 newVector) => t.position = V(t.position.x, newVector.y, newVector.z);
        public static void SetXorZ(this Transform t, bool b, float value) => t.position = b ? V(value, t.position.y, t.position.z) : V(t.position.x, t.position.y, value);
        public static bool IsAimAxisZ(this Transform t) => t.forward.z > t.forward.x;
    }
}