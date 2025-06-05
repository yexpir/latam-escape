using System.Collections.Generic;
using System.Linq;
using CityStuff.ManagerStuff;
using Extensions;
using Gameplay;
using UnityEngine;
using Utils;

namespace CityStuff.SpawnerStuff
{
    public class SpawnMasker
    {
        public readonly List<SpawnArea> areas;
        public readonly SpawnArea mainArea;
        public readonly Character follow;
        //readonly HashSet<Vector2Int> chunksInsideArea = new();

        public SpawnMasker(Character follow)
        {
            this.follow = follow;
            areas = City.spawnAreas.spawnAreas.Select(e => new SpawnArea(e)).ToList();
            mainArea = areas.OrderByDescending(e => Vector2.Distance(e.endpoints[0], e.endpoints[1])).FirstOrDefault();
        }

        public HashSet<int> GetChunkFilter(Vector2 coordinates)
        {
            return areas
                .Where(area => area.IsInside(coordinates))
                .Select(area => area.layer).ToHashSet();
        }
        
        public HashSet<Vector2Int> GetChunksInside()
        {
            //chunksInsideArea.Clear();
            var chunksInsideArea = new HashSet<Vector2Int>();
            mainArea.UpdateAreaFollow(follow.state.currentChunkCoordinates);
            var bottomLeft = mainArea.bottomLeft;
            var topRight = mainArea.topRight;

            for (var y = bottomLeft.y; y < topRight.y; y++)
            {
                for (var x = bottomLeft.x; x < topRight.x; x++)
                {
                    chunksInsideArea.Add(new Vector2Int(x, y));
                }
            }

            return chunksInsideArea;
        }

    }
}