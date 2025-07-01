using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CityStuff.ConfigurationStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
using Unity.VisualScripting;
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
            poolDict.Clear();
            for (var i = 0; i < worldObjectSet.poolEntries.Count; i++)
            {
                var poolEntry = worldObjectSet.poolEntries[i];
                var i1 = i;
                poolDict[(uint)i] = new ObjectPool<WorldObject>(
                    () =>
                    {
                        var o = Object.Instantiate(poolEntry.prefab);
                        o.SetId((uint)i1);
                        return o;
                    },
                    obj => obj.OnGet(),
                    obj => obj.OnRelease(),
                    Object.Destroy,
                    false,
                    poolEntry.defaultSize,
                    poolEntry.maxSize
                );
            }
            // foreach (var poolEntry in worldObjectSet.poolEntries)
            // {
            //     poolDict[poolEntry.id] = new ObjectPool<WorldObject>(
            //         () =>
            //         {
            //             var o = Object.Instantiate(poolEntry.prefab);
            //             return o;
            //         },
            //         obj => obj.OnGet(),
            //         obj => obj.OnRelease(),
            //         Object.Destroy,
            //         false,
            //         poolEntry.defaultSize,
            //         poolEntry.maxSize
            //     );
            // }
        }
        public static WorldObject Get(uint id)
        {
            return poolDict.ContainsKey(id) ? poolDict[id].Get() : null;
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