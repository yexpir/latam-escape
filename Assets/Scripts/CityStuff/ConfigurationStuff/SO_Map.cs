using System;
using EditorAttributes;
using UnityEngine;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New Map", menuName = "Map")]
    [Serializable]
    public class SO_Map : ScriptableObject
    {
        public float cellSize;
        public int buildingSize;
        public int laneCount;
        public int laneWidth;
        public int streetEdge;
        public int granularity;
        public float heightPow;
        public float heightMult;
        public int totalLanesWidth { get; private set; }
        public int totalStreetWidth { get; private set; }
        public int blockSize { get; private set; }

        void OnValidate()
        {
            totalLanesWidth = (laneCount-1) * laneWidth + 1;
            totalStreetWidth = totalLanesWidth + streetEdge * 2;
            blockSize = buildingSize + totalStreetWidth;
        }

        public event Action OnMapValidated;
        [Button("Apply")]
        void Apply()
        {
            OnMapValidated?.Invoke();
        }
    }
}