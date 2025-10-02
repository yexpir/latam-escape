using System.Collections.Generic;
using System.Linq;
using CityStuff.WorldDataStuff;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    public static class ChunkSpawner
    {
        //here it filters all the object datas in the chunk data by layer
        //and then spawns them
        
        //check discrepancy between filter and chunk layers, and spawn/despawn accordingly

        public static void Spawn(ChunkData chunk, HashSet<int> filter, Transform parent)
        {
            chunk.AddWorldObject(ObjectSpawner.Spawn(chunk.chunkContainerData, parent));
            var objectDatas = chunk.GetObjectDatas(filter).ToArray();
            foreach (var obj in objectDatas)
            {
                chunk.AddWorldObject(ObjectSpawner.Spawn(obj, chunk.chunkContainer.transform));
            }
        }

        public static void Despawn(ChunkData chunk)
        {
            foreach (var obj in chunk.activeWorldObjects.ToList())
            {
                chunk.RemoveWorldObject(obj);
                ObjectSpawner.DeSpawn(obj.data);
            }
            ObjectSpawner.DeSpawn(chunk.chunkContainerData);
            chunk.RemoveChunkContainer();
        }
    }
}