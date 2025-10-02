using System.Collections.Generic;
using System.Linq;
using CityStuff.GenerationStuff;
using CityStuff.PrefabStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
using Extensions;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace CityStuff.WorldDataStuff
{
    public class ChunkData
    {
        public Vector2Int coordinates { get; }
        public Vector3 position { get; }
        public bool isActive { get; private set; }
        public int uid { get; }
        public static int chunkCount { get; private set; }
        
        public HashSet<int> filter = new();


        Dictionary<int , ObjectData> _objectDatas = new();
        public IReadOnlyDictionary<int, ObjectData> objectDatas => _objectDatas;
        public readonly Dictionary<int, Dictionary<int, ObjectData>> objectDatasByLayer = new();
        
        
        public readonly HashSet<WorldObject> activeWorldObjects = new();

        public ObjectData chunkContainerData;
        public ChunkContainer chunkContainer;

        public ChunkData(Vector2Int newChunkCoordinates)
        {
            chunkCount++;
            uid = chunkCount;  
            coordinates = newChunkCoordinates;
            position = MapCalculator.CellToWorld(coordinates);
            //procedural algorithm call here
            
            //CHUNK CONTAINER
            CreateChunkContainerData();
            
            //BLOCK
            CreateObjectData(0, IDManager.blockID);
            
            var segments = City.segmentSet.segments;
            if (segments.Count == 0) return;
            
            //STREET 1
            var index = Random.Range(0, segments.Count);
            var segment = segments[index];
            foreach (var piece in segment.pieces)
                CreateObjectData(1, piece.Item1, piece.Item2, piece.Item3);
            
            //STREET 2
        }

        void CreateChunkContainerData()
        {
            chunkContainerData = new ObjectData(0, IDManager.chunkContainerID, coordinates);
        }

        void CreateObjectData(int hierarchy, int id, Vector3 wobjPosition = default, Quaternion wobjRotation = default)
        {
            var newobj = new ObjectData(hierarchy, id, coordinates, wobjPosition, wobjRotation);
            _objectDatas.Add(newobj.uid, newobj);
            if (!objectDatasByLayer.ContainsKey(newobj.layer))
                objectDatasByLayer.Add(newobj.layer, new Dictionary<int, ObjectData>());
            objectDatasByLayer[newobj.layer].Add(newobj.uid, newobj);
        }

        public IEnumerable<ObjectData> GetObjectDatas(HashSet<int> newFilter)
        {
            filter = newFilter;
            var layers = new HashSet<int>(filter);
            var filteredObjectDatas = objectDatasByLayer
                .Where(k => layers.Contains(k.Key))
                .Select(k => k.Value)
                .SelectMany(d => d.Values).ToList();
            return filteredObjectDatas;
        }

        public void UpdateObjectData(int uid)
        {
            
        }

        public void DeleteObjectData(int uid)
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

        public void AddChunkContainer(ChunkContainer newChunkContainer)
        {
            chunkContainer = newChunkContainer;
        }
        
        public void RemoveChunkContainer()
        {
            chunkContainer = null;
        }
    }
}