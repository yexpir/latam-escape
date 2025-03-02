using UnityEngine;
using System.Collections.Generic;
using ProceduralToolkit.Runtime.Buildings;

namespace ProceduralToolkit.Buildings
{
    public abstract class FacadePlanner : ScriptableObject, IFacadePlanner
    {
        public abstract List<ILayout> Plan(List<Vector2> foundationPolygon, BuildingGenerator.Config config);
    }
}
