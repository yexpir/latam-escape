using System;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    [Serializable]
    public struct SpawnAreaEntry
    {
        [SerializeField] public int layer;
        [SerializeField] public Vector2Int[] endpoints;
        [SerializeField] public Vector2Int offset;
    }
}