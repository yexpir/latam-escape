using System.Collections.Generic;
using System.Linq;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using UnityEngine;

namespace CityStuff.GenerationStuff.BlockGenerationStuff.Mesh
{
    public static class MeshGenerator
    {
        public static UnityEngine.Mesh Generate(int bitmask)
        {
            return QuadsToMesh(BlockData.GetQuads(bitmask).ToArray());
        }

        static UnityEngine.Mesh QuadsToMesh(IReadOnlyList<Quad> quads)
        {
            var mesh = new UnityEngine.Mesh();

            var vertices = new List<Vector3>();
            var normals = new List<Vector3>();
            var uvs = new List<Vector2>();
            var triangles = new List<int>();

            for (var i = 0; i < quads.Count; i++)
            {
                vertices.AddRange(quads[i].Vertices);
                normals.AddRange(quads[i].Normals);
                uvs.AddRange(Quad.Uvs);
                triangles.AddRange(Quad.Triangles.Select(n => n + i*4));
            }

            mesh.vertices = vertices.ToArray();
            mesh.normals = normals.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = triangles.ToArray();
            
            return mesh;
        }
    }
}