using System;
using UnityEngine;

namespace CityGeneration.Data
{
    [CreateAssetMenu(fileName = "New Block", menuName = "Block")]
    public class SO_Block : ScriptableObject
    {
        public float minSaturation;

        void OnValidate()
        {
            minSaturation = Mathf.Clamp(minSaturation, 0.0f, 1.0f);
        }
    }
}