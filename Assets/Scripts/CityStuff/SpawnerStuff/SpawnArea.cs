using System;
using Extensions;
using Gameplay;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    [Serializable]
    public class SpawnArea
    {
        public int layer;
        public Vector2Int bottomLeft => endpoints[0] + _character.state.currentChunk;
        public Vector2Int topRight => endpoints[1] + _character.state.currentChunk;
        
        public Vector2Int[] endpoints;
        
        Character _character;
        
        public SpawnArea(SpawnAreaEntry entry, Character character)
        {
            layer = entry.layer;
            endpoints = entry.endpoints.AddToAll(entry.offset);
            _character = character;
        }
        
        void OffsetEndpoints(Vector2Int offset)
        {
            endpoints.AddToAll(offset);
        }

        public bool IsInside(Vector2 v)
        {
            var e1 = bottomLeft;
            var e2 = topRight;
            
            return v.x >= e1.x && v.y >= e1.y && v.x < e2.x && v.y < e2.y;
        }
    }
}