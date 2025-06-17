using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static bool CompareLayerMask(this GameObject go, LayerMask layerMask)
        {
            return ((1 << go.layer) & layerMask) != 0;
        }
    }
}