using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using CityStuff.WorldDataStuff;
using Extensions;
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
                chunk.AddWorldObject(ObjectSpawner.Spawn(obj, parent));
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