using System;
using CityStuff.GenerationStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using UnityEngine;

namespace CityStuff.WorldDataStuff
{
    [Serializable]
    public class ObjectData
    {
        public static uint objCount { get; private set; }
        public string name { get; private set; }
        public uint uid { get; private set; }
        public uint id { get; private set; }
        public Vector2Int coordinates { get; private set; }
        public Vector3 position { get; private set; }

        public int layer;

        public WorldObject worldObject { get; private set; }

        public bool isActive { get; private set; }

        public ObjectData(uint id, Vector2Int coordinates, string name)
        {
            this.id = id;
            this.coordinates = coordinates;
            this.name = name;
            position = MapCalculator.CellToWorld(coordinates);
            objCount++;
            uid = objCount;
        }

        public void Load(WorldObject obj)
        {
            isActive = true;
            worldObject = obj;
        }

        public void Unload()
        {
            isActive = false;
            worldObject = null;
        }
    }
}