using System.Collections.Generic;
using System.Linq;
using CityStuff.PrefabStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
using CityStuff.WorldDataStuff;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    public static class WorldSpawner
    {
        public static void Spawn(WorldData world, SpawnMasker masker, Transform parent)
        {
            //bro just spawn all the chunk containers first XD
            
            var areaChunks = masker.GetChunksInside();
            var activeChunks = world.activeChunks;
            
            var chunksToDespawn = new HashSet<Vector2Int>(activeChunks);
            chunksToDespawn.ExceptWith(areaChunks);

            var chunksToSpawn = new HashSet<Vector2Int>(areaChunks);
            chunksToSpawn.ExceptWith(activeChunks);

            activeChunks.ExceptWith(chunksToDespawn);
            activeChunks.UnionWith(chunksToSpawn);

            var chunkDatasToDespawn = chunksToDespawn.Select(world.GetChunkData).ToList();
            var chunkDatasToSpawn = chunksToSpawn.Select(world.GetChunkData).ToList();

            var chunkContainerDatasToDespawn = chunkDatasToDespawn.Select(d => d.chunkContainerData);
            var chunkContainerDatasToSpawn = chunkDatasToSpawn.Select(d => d.chunkContainerData);

            
            //despawn previous chunks
            foreach (var chunkData in chunkDatasToSpawn)
            {
                ChunkSpawner.Despawn(chunkData);
            }
            
            //despawn previous chunk containers
            foreach (var chunkContainerData in chunkContainerDatasToDespawn)
            {
                world.RemoveChunkContainer(chunkContainerData.chunkContainer);
                ObjectSpawner.DeSpawn(chunkContainerData);
            }
            
            //spawn next chunk containers
            foreach (var chunkContainerData in chunkContainerDatasToSpawn)
                world.AddChunkContainer(ObjectSpawner.Spawn(chunkContainerData, parent) as ChunkContainer);

            //spawn next chunks
            foreach (var chunk in chunkDatasToDespawn)
            {
                var filter = masker.GetChunkFilter(chunk.coordinates);
                ChunkSpawner.Spawn(chunk, filter, parent);
            }
        }
    }
}