using System;
using EditorAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New Perlin", menuName = "Perlin")]
    [Serializable]
    public class SO_Perlin : ScriptableObject
    {
        public int seed;
        public float scale;
        public Vector2 origin;

        public event Action OnPerlinValidated;
        [Button("Apply")]
        void Apply()
        {
            OnPerlinValidated?.Invoke();
        }
        
        public void SetOrigin()
        {
            Random.InitState(seed);
            origin = new Vector2(Random.Range(-10000, 10000), Random.Range(-10000, 10000));
        }

        public void SetSeed(int newSeed)
        {
            seed = newSeed;
        }

        public void SetSeed()
        {
            seed = RandomSeed;
        }

        static int RandomSeed => Random.Range(int.MinValue, int.MaxValue);
    }
}