using System;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;

namespace CityStuff.PoolStuff
{
    [Serializable]
    public class PoolEntry
    {
        public uint id;
        public WorldObject prefab;
        public int defaultSize;
        public int maxSize;
    }
}