using System.Collections.Generic;
using System.Linq;
using CityStuff.WorldDataStuff;
using UnityEngine;
using Utils;

namespace CityStuff.SpawnerStuff
{
    public static class WorldSpawner
    {
        public static void Spawn(WorldData world, SpawnMasker masker, Transform parent)
        {
            MyLogger.Log("SPAWN!");
            var areaChunks = masker.GetChunksInside();
            var activeChunks = world.activeChunks;
            
            var chunksToDespawn = new HashSet<Vector2Int>(activeChunks);
            chunksToDespawn.ExceptWith(areaChunks);

            
            var chunksToSpawn = new HashSet<Vector2Int>(areaChunks);
            chunksToSpawn.ExceptWith(activeChunks);


            activeChunks.ExceptWith(chunksToDespawn);
            activeChunks.UnionWith(chunksToSpawn);
            

            foreach (var chunkData in chunksToDespawn.Select(world.GetChunkData))
            {
                ChunkSpawner.Despawn(chunkData);
            }

            foreach (var chunk in chunksToSpawn)
            {
                var chunkData = world.GetChunkData(chunk);
                var filter = masker.GetChunkFilter(chunk);
                ChunkSpawner.Spawn(chunkData, filter, parent);
            }
            
            world.activeChunks = activeChunks;
        }
    }
}