using System.Collections.Generic;
using CityStuff.SpawnerStuff;
using UnityEngine;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New Spawn Areas", menuName = "SpawnArea")]

    public class SO_SpawnAreas : ScriptableObject
    {
        [SerializeField] public List<SpawnAreaEntry> spawnAreas;
    }
}