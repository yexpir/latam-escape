using System.Collections.Generic;

namespace CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data
{
    public static class MeshData
    {
        public static UnityEngine.Mesh GetMesh(int bitmask)
        {
            return meshes[bitmask];
        }

        public static void Init() => GenerateMeshes();
        public static void GenerateMeshes()
        {
            meshes.Clear();
            foreach (var key in BlockData.blocks.Keys)
            {
                meshes.Add(key, MeshGenerator.Generate(key));
            }
        }

        static readonly Dictionary<int, UnityEngine.Mesh> meshes = new();
    }
}