using System;
using System.Linq;
using CityStuff.GenerationStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
using UnityEngine;
using Utils;

namespace CityStuff.WorldDataStuff
{
    [Serializable]
    public class ObjectData
    {
        public static int objCount { get; private set; }
        public string name { get; private set; }
        public int uid { get; private set; }
        public int id { get; private set; }
        public Vector2Int chunkCoordinates { get; private set; }
        public Vector3 position { get; private set; }
        public Quaternion rotation { get; private set; }

        public int layer;
        public int hierarchy;//0=child of world, 1=child of chunkContainer, 2=child of segment1, 3=child of segment2

        WorldObject _worldObject;
        public WorldObject worldObject
        {
            get => hierarchy == 0 ? chunkContainer : _worldObject;
            private set => _worldObject = value;
        }

        public ChunkContainer chunkContainer { get; private set; }
        public bool isActive { get; private set; }

        public ObjectData(int hierarchy, int id, Vector2Int chunkCoordinates, Vector3 wobjPosition = default, Quaternion wobjRotation = default)
        {
            this.hierarchy = hierarchy;
            this.id = id;
            this.chunkCoordinates = chunkCoordinates;
            position = MapCalculator.CellToWorld(chunkCoordinates) + wobjPosition;
            rotation = wobjRotation;
            objCount++;
            uid = objCount;
            name = City.worldObjectSet.poolEntries.FirstOrDefault(o => o.id == id)?.prefab.name;
        }

        // public void Set(int? newHierarchy = null, int? newId = null, Vector2Int? newChunkCoordinates = null, Vector3? newPosition = null, Quaternion? newRotation = null)
        // {
        //     hierarchy = newHierarchy ?? hierarchy;
        //     id = newId ?? id;
        //     chunkCoordinates = newChunkCoordinates ?? chunkCoordinates;
        //     position = newPosition ?? position;
        //     rotation = newRotation ?? rotation;
        // }

        public void Load(WorldObject obj)
        {
            isActive = true;
                worldObject = obj;
            worldObject.Init(this);
        }

        public void Unload()
        {
            isActive = false;
            worldObject = null;
        }

        public override string ToString()
        {
            return $"object name: {name} object id: {id}";
        }
    }
}