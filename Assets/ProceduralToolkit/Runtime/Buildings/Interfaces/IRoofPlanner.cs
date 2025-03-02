using UnityEngine;
using System.Collections.Generic;
using ProceduralToolkit.Runtime.Buildings;

namespace ProceduralToolkit.Buildings
{
    public interface IRoofPlanner
    {
        IConstructible<MeshDraft> Plan(List<Vector2> foundationPolygon, BuildingGenerator.Config config);
    }
}
