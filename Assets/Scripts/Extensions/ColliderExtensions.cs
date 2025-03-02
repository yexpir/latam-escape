using UnityEngine;

namespace Extensions
{
    public static class ColliderExtensions
    {
        public static bool CompareLayer(this Collider other, int layerValue)
        {
            return (layerValue & (1 << other.gameObject.layer)) > 0;
        }
    }
}