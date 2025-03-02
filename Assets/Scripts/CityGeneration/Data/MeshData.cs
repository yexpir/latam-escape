using System.Collections.Generic;
using CityGeneration.MeshGeneration;
using Extensions;
using UnityEngine;

namespace CityGeneration.Data
{
    public static class MeshData
    {
        public static Mesh GetMesh(int bitmask)
        {
            return meshes[bitmask];
        }
        
        public static void GenerateMeshes()
        {
            meshes.Clear();
            foreach (var key in BlockData.blocks.Keys)
            {
                meshes.Add(key, MeshGenerator.Generate(key));
            }
        }

        static readonly Dictionary<int, Mesh> meshes = new();
    }
}