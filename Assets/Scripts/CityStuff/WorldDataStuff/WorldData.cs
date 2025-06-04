using System;
using System.Collections.Generic;
using UnityEngine;

namespace CityStuff.WorldDataStuff
{
    public class WorldData
    {
        Dictionary<Vector2Int, ChunkData> _chunkDatas = new();
        public IReadOnlyDictionary<Vector2Int, ChunkData> chunkDatas => _chunkDatas;

        public HashSet<Vector2Int> activeChunks = new();

        public ChunkData GetChunkData(Vector2Int coordinates)
        {
            if (!_chunkDatas.ContainsKey(coordinates))
                _chunkDatas.Add(coordinates, new ChunkData(coordinates));
            return _chunkDatas[coordinates];
        }
    }
}