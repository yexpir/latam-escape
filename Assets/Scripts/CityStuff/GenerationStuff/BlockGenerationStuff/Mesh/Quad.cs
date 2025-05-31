using System;
using System.Collections.Generic;
using System.Linq;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using UnityEngine;

namespace CityStuff.GenerationStuff.BlockGenerationStuff.Mesh
{
    public struct Quad
    {
        int[] _indexes;
        Vector3 _normal;
        
        public Vector3[] Vertices { get; private set;}
        public Vector3[] Normals { get;  private set;}

        public static int[] Triangles { get; } = {
            0, 3, 1,
            0, 2, 3
        };

        public static Vector2[] Uvs { get; } =
        {
            new(0, 0), //bot-left
            new(1, 0), //bot-right
            new(0, 1), //top-left
            new(1, 1)  //top-right
        };


        public Quad(int[] indexes, Vector3 normal)
        {
            if (indexes.Length != 4)
                throw new ArgumentException("Indexes should be length 4");

            _indexes = indexes;
            _normal = normal;
            
            Vertices = new[] {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
            Normals = new[] {Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero};
        }

        public void Init() 
        {
            Vertices = GetVertices(_indexes);
            Normals = GetNormals(_normal);
        }

        static Vector3[] GetVertices(IEnumerable<int> indexes)
        {
            return indexes.Select(i => QuadData.vertices[i]).ToArray();
        }

        static Vector3[] GetNormals(Vector3 normal)
        {
            var newNormals = new Vector3[4];
            for (var i = 0; i < 4; i++)
                newNormals[i] = normal;
            return newNormals;
        }
    }
}