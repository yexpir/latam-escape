using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityStuff.PrefabStuff.BaseObjectStuff
{
    public class Segment
    {
        public readonly List<Tuple<int, Vector3, Quaternion>> pieces = new();
        public Segment(IEnumerable<WorldObject> wobjs)
        {
            foreach (var wobj in wobjs)
                pieces.Add(new Tuple<int, Vector3, Quaternion>(wobj.id, wobj.transform.position, wobj.transform.rotation));
        }
    }
}