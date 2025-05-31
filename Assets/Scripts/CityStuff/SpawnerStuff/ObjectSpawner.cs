using System.Transactions;
using CityStuff.PoolStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using CityStuff.WorldDataStuff;
using UnityEngine;

namespace CityStuff.SpawnerStuff
{
    public static class ObjectSpawner
    {
        //here it finds the object with the id
        //and requests it from the object pool

        public static WorldObject Spawn(ObjectData obj, Transform parent)
        {
            obj.Load(PoolerMain.Get(obj.id));
            obj.worldObject.Init(obj, parent);
            return obj.worldObject;
        }

        public static void DeSpawn(ObjectData obj)
        {
            PoolerMain.Release(obj.worldObject);
            obj.Unload();
        }
    }
}