using System.Collections.Generic;
using CityStuff.ConfigurationStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using UnityEngine;
using UnityEngine.Pool;
using Utils;

namespace CityStuff.PoolStuff
{
    public static class PoolerMain
    {
        public static SO_WorldObjectSet worldObjectSet;
        public static readonly Dictionary<uint, ObjectPool<WorldObject>> poolDict = new();

        public static void Init()
        {
            worldObjectSet = City.worldObjectSet;
            foreach (var poolEntry in worldObjectSet.poolEntries)
            {
                poolDict[poolEntry.id] = new ObjectPool<WorldObject>(
                    () => Object.Instantiate(poolEntry.prefab),
                    obj => obj.OnGet(),
                    obj => obj.OnRelease(),
                    Object.Destroy,
                    false,
                    poolEntry.defaultSize,
                    poolEntry.maxSize
                );
            }
        }
        public static WorldObject Get(uint id)
        {
            return !poolDict.ContainsKey(id) ? null : poolDict[id].Get();
        }

        public static void Release(WorldObject obj)
        {
            if (poolDict.TryGetValue(obj.id, out var pool))
            {
                pool.Release(obj);
            }
        }
    }
}