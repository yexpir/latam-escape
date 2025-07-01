using System.Transactions;
using CityStuff.PoolStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
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
            var wobj = PoolerMain.Get(obj.id);
            obj.Load(wobj);
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