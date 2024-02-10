using UnityEngine;

namespace WIP.Utils
{
    public static class V
    {
        public static Vector3 Flat(this Vector3 v)
        {
            v.y = 0;
            return v;
        }
    }
}