using System.Collections.Generic;
using System.Linq;
using CityStuff.WorldDataStuff;
using Extensions;
using Unity.Mathematics;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    public static class ChunkSpawner
    {
        //here it filters all the object datas in the chunk data by layer
        //and then spawns them

        public static void Spawn(ChunkData chunk, HashSet<int> filter, Transform parent)
        {
            //check discrepancy between filter and chunk layers, and spawn/despawn accordingly
            var objectDatas = chunk.GetObjectDatas(filter).ToArray();
            foreach (var obj in objectDatas)
            {
                var wobj = ObjectSpawner.Spawn(obj, parent);
                chunk.AddWorldObject(wobj);
            }
        }

        public static void Despawn(ChunkData chunk)
        {
            foreach (var obj in chunk.activeWorldObjects.ToList())
            {
                chunk.RemoveWorldObject(obj);
                ObjectSpawner.DeSpawn(obj.data);
            }
        }
    }
}