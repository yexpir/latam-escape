using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Utils
{
    public class Street
    {
        public Vector3 axis { get; private set; }
        public float street { get; private set; }
        
        public Vector3 streetPosition => axis * street;
        
        public float[] lanes => StreetService.GetStreetLanes(street).ToArray();
        public IEnumerable<Vector3> lanesPositions => lanes.Select(l => Vector3.zero.ProjectValueWithSelector(l, axis)).ToArray();

        public Street(Vector3 newAxis, float newStreet) => Set(newAxis, newStreet);

        public Street(){}

        public void Set(Vector3 newAxis, float newStreet)
        {
            axis = newAxis.normalized.Abs().Round();
            street = newStreet;
        }

        public Vector3 GetLane(int index) => lanesPositions.ToArray()[index];

        public override string ToString() => $"STREET: {street} AXIS: {axis}";
    }
}