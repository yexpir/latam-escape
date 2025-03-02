using System;
using EditorAttributes;
using UnityEngine;

namespace CityGeneration.Data
{
    [CreateAssetMenu(fileName = "New City", menuName = "City")]
    public class SO_City : ScriptableObject
    {
        public SO_Map mapData;
        public SO_Perlin perlinData;
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