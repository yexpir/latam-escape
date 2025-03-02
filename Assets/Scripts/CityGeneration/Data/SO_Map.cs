using System;
using EditorAttributes;
using UnityEngine;

namespace CityGeneration.Data
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
        public int totalLanesWidth => (laneCount-1) * laneWidth + 1;
        public int totalStreetWidth => totalLanesWidth + streetEdge * 2;//laneCount + (laneCount-1) * (laneWidth-1) + (streetEdge * 2);
        public int blockSize => buildingSize + totalStreetWidth;
        public int granularity;
        public float heightPow;
        public float heightMult;

        public event Action OnMapValidated;
        [Button("Apply")]
        void Apply()
        {
            OnMapValidated?.Invoke();
        }
    }
}