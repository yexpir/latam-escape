using System;
using Extensions;
using Gameplay;
using UnityEngine;
using Utils;

namespace CityStuff.SpawnerStuff
{
    [Serializable]
    public class SpawnArea
    {
        public int layer;
        public Vector2Int bottomLeft { get; private set; }
        public Vector2Int topRight { get; private set; }
        
        public Vector2Int[] endpoints;
        
        public SpawnArea(SpawnAreaEntry entry)
        {
            layer = entry.layer;
            endpoints = entry.endpoints.AddToAll(entry.offset);
        }

        public void UpdateAreaFollow(Vector2Int follow)
        {
            bottomLeft = endpoints[0] + follow;
            topRight = endpoints[1] + follow;
        }

        public bool IsInside(Vector2 v)
        {
            var e1 = bottomLeft;
            var e2 = topRight;
            
            return v.x >= e1.x && v.y >= e1.y && v.x < e2.x && v.y < e2.y;
        }
    }
}