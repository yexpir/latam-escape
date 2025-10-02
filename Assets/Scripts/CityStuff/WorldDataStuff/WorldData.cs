using System.Collections.Generic;
using System.Linq;
using CityStuff.PrefabStuff.BaseObjectStuff;
using UnityEngine;

namespace CityStuff.WorldDataStuff
{
    public class WorldData
    {
        readonly Dictionary<Vector2Int, ChunkData> _chunkDatas = new();
        public IReadOnlyDictionary<Vector2Int, ChunkData> chunkDatas => _chunkDatas;

        public readonly HashSet<Vector2Int> activeChunks = new();

        public static Dictionary<Vector2Int, ChunkContainer> chunkContainers = new();

        public ChunkData GetChunkData(Vector2Int coordinates)
        {
            if (!_chunkDatas.ContainsKey(coordinates))
                _chunkDatas.Add(coordinates, new ChunkData(coordinates));
            return _chunkDatas[coordinates];
        }

        public void SetContainers()
        {
            chunkContainers = _chunkDatas
                .SelectMany(d => d.Value.activeWorldObjects)
                .OfType<ChunkContainer>()
                .ToDictionary(w => w.data.chunkCoordinates, w => w);
            Debug.Log("Set Containers");
        }

        public void AddChunkContainer(ChunkContainer container)
        {
            chunkContainers.Add(container.data.chunkCoordinates, container);
        }
        public void RemoveChunkContainer(ChunkContainer container)
        {
            chunkContainers.Add(container.data.chunkCoordinates, container);
        }

        void CreateContainerData()
        {
            
        }
    }
}