using System.Linq;
using Extensions;
using UnityEngine;

namespace CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data
{
    public static class QuadData
    {
        public static Vector3[] vertices { get; private set; }

        public static void Init()
        {
            SetVertices();
            for (var i = 0; i < quads.Length; i++)
                quads[i].Init();
        }

        static void SetVertices()
        {
            var botVertices = ShapeData.vertices;
            var topVertices = botVertices.Select(v => v.SetY(1f));
            var newVertices = botVertices.Concat(topVertices).ToList();
            newVertices.RemoveRange(0, 4);
            vertices = newVertices.ToArray();
        }

        public static readonly Quad[] quads =
        {
            //walls
            new(new[] { 2,  1, 18, 17}, Vector3.back),    //south - left
            new(new[] { 1, 10, 17, 26}, Vector3.back),    //south - center
            new(new[] {10,  9, 26, 25}, Vector3.back),    //south - right
            new(new[] {11, 10, 27, 26}, Vector3.right),   //east - left
            new(new[] {10,  7, 26, 23}, Vector3.right),   //east - center
            new(new[] { 7,  6, 23, 22}, Vector3.right),   //east - right
            new(new[] { 8,  7, 24, 23}, Vector3.forward), //north - left
            new(new[] { 7,  4, 23, 20}, Vector3.forward), //north - center
            new(new[] { 4,  3, 20, 19}, Vector3.forward), //north - right
            new(new[] { 5,  4, 21 ,20}, Vector3.left),    //west - left
            new(new[] { 4,  1, 20, 17}, Vector3.left),    //west - center
            new(new[] { 1,  0, 17, 16}, Vector3.left),    //west - right
            //roofs
            new(new[] {12, 16, 18, 17}, Vector3.up), //bottom left
            new(new[] {16, 27, 17, 26}, Vector3.up), //bottom
            new(new[] {27, 15, 26, 25}, Vector3.up), //bottom right
            new(new[] {26 ,25, 23, 24}, Vector3.up), //right
            new(new[] {23, 24, 22, 14}, Vector3.up), //top right
            new(new[] {20, 23, 21, 22}, Vector3.up), //top
            new(new[] {19, 20, 13, 21}, Vector3.up), //top left
            new(new[] {18, 17, 19, 20}, Vector3.up), //left
            new(new[] {17, 26, 20, 23}, Vector3.up)  //center
        };
    }
}