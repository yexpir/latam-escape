using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Samples.Buildings.Runtime
{
    [CreateAssetMenu(menuName = "ProceduralToolkit/Buildings/Foundation Polygon", order = 0)]
    public class PolygonAsset : ScriptableObject
    {
        public List<Vector2> vertices = new List<Vector2>();

        public void SetVertices(List<Vector2> list)
        {
            vertices = list;
        }
    }
}
