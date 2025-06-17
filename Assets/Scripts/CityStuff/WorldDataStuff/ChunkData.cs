using System;
using System.Collections.Generic;
using System.Linq;
using CityStuff.GenerationStuff;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using Extensions;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CityStuff.WorldDataStuff
{
    public class ChunkData
    {
        public Vector2Int coordinates { get; }
        public Vector3 position { get; }
        public bool isActive { get; private set; }
        public uint uid { get; }
        
        public HashSet<int> filter = new();
        

        Dictionary<uint , ObjectData> _objectDatas = new();
        public IReadOnlyDictionary<uint, ObjectData> objectDatas => _objectDatas;
        public readonly Dictionary<int, Dictionary<uint, ObjectData>> objectDatasByLayer = new();
        
        
        public readonly HashSet<WorldObject> activeWorldObjects = new();


        public ChunkData(Vector2Int newposition)
        {
            chunkCount++;
            uid = chunkCount;  
            coordinates = newposition;
            position = MapCalculator.CellToWorld(coordinates);
            //procedural algorithm call here
            CreateObjectData(0, coordinates, "block");
            var rnd = (uint)Random.Range(1, 3);
            CreateObjectData(rnd, coordinates, "street1");
        }

        public void CreateObjectData(uint id, Vector2Int coordinates, string name)
        {
            var newobj = new ObjectData(id, coordinates, name);
            if (!objectDatasByLayer.ContainsKey(newobj.layer))
                objectDatasByLayer.Add(newobj.layer, new Dictionary<uint, ObjectData>());
            objectDatasByLayer[newobj.layer].Add(newobj.uid, newobj);
        }

        public IEnumerable<ObjectData> GetObjectDatas(HashSet<int> filter)
        {
            this.filter = filter;
            var layers = new HashSet<int>(filter);
            var filteredObjectDatas = objectDatasByLayer
                .Where(k => layers.Contains(k.Key))
                .Select(k => k.Value)
                .SelectMany(d => d.Values).ToList();
            return filteredObjectDatas;
        }

        public void UpdateObjectData(uint uid)
        {
            
        }

        public void DeleteObjectData(uint uid)
        {
            _objectDatas.Remove(uid);
        }   

        public void AddWorldObject(WorldObject obj)
        {
            if (activeWorldObjects.Count == 0)
                isActive = true;
            activeWorldObjects.Add(obj);
        }

        public void RemoveWorldObject(WorldObject obj)
        {
            activeWorldObjects.Remove(obj);
            if (activeWorldObjects.Count == 0)
                isActive = false;
        }
        
        public static uint chunkCount { get; private set; }
    }
}