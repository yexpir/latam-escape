using System;
using CityStuff.PrefabStuff;
using EditorAttributes;
using UnityEngine;
using Utils;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New City", menuName = "City")]
    public class SO_City : ScriptableObject
    {
        public SO_Map mapData;
        public SO_Perlin perlinData;
        public SO_Block blockData;
        public SO_SpawnAreas spawnAreas;
        public SO_WorldObjectSet worldObjectSet;
        public SO_SegmentSet segmentSet;
        public Block blockPrefab;
        public Vector2 area;
        
        public event Action OnCityValidated;

        void OnEnable()
        {
            mapData.OnMapValidated += InvokeOnCityValidated;
            perlinData.OnPerlinValidated += InvokeOnCityValidated;
        }

        [Button("Apply")]
        void Apply()
        {
            InvokeOnCityValidated();
        }

        void InvokeOnCityValidated()
        {
            OnCityValidated?.Invoke();
        }
    }
}