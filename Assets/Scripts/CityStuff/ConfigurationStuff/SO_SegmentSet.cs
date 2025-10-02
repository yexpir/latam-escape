using System.Collections.Generic;
using System.Linq;
using CityStuff.PrefabStuff.BaseObjectStuff;
using EditorAttributes;
using UnityEngine;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New Segment Set", menuName = "SegmentSet")]
    public class SO_SegmentSet : ScriptableObject
    {
        public List<WorldObject> entries = new();
        [HideInInspector] public List<Segment> segments = new();

        [Button("Apply")]
        public void Init()
        {
            segments = entries.Select(entry 
                => new Segment(entry.GetComponentsInChildren<WorldObject>())).ToList();
        }
    }
}