using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;
using Utils;

namespace CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data
{
    public static class ShapeData
    {
        public static float A { get; private set; } // inner square
        public static float B { get; private set; } // outer square

        public static void Init()
        {
            A = City.map.buildingSize / 2f * City.map.cellSize;
            B = (City.map.buildingSize + City.map.totalStreetWidth) / 2f * City.map.cellSize;
            UpdateVerticesCW();
        }

        public static readonly Vector3[] vertices = {
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero,
            Vector3.zero
        };
        static void UpdateVerticesCW()
        {
            vertices[0].SetFlat(-B, -B);
            vertices[1].SetFlat(-B, B);
            vertices[2].SetFlat(B, B);
            vertices[3].SetFlat(B, -B);
            
            vertices[4].SetFlat(-A, -B);
            vertices[5].SetFlat(-A, -A);
            vertices[6].SetFlat(-B, -A);
            
            vertices[7].SetFlat(-B, A);
            vertices[8].SetFlat(-A, A);
            vertices[9].SetFlat(-A, B);
            
            vertices[10].SetFlat(A, B);
            vertices[11].SetFlat(A, A);
            vertices[12].SetFlat(B, A);
            
            vertices[13].SetFlat(B, -A);
            vertices[14].SetFlat(A, -A);
            vertices[15].SetFlat(A, -B);

            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] += Vector3.one.Flatten() * (MapCalculator.ConvertValue/2);
            }
        }
        
        static void UpdateVerticesCCW()
        {
            vertices[0].SetFlat(-B, -B);
            vertices[1].SetFlat(B, -B);
            vertices[2].SetFlat(B, B);
            vertices[3].SetFlat(-B, B);
            
            vertices[4].SetFlat(-B, -A);
            vertices[5].SetFlat(-A, -A);
            vertices[6].SetFlat(-A, -B);
            
            vertices[7].SetFlat(A, -B);
            vertices[8].SetFlat(A, -A);
            vertices[9].SetFlat(B, -A);
            
            vertices[10].SetFlat(B, A);
            vertices[11].SetFlat(A, A);
            vertices[12].SetFlat(A, B);
            
            vertices[13].SetFlat(-A, B);
            vertices[14].SetFlat(-A, A);
            vertices[15].SetFlat(-B, A);
        }
        
        static IEnumerable<Vector3> GetShapeAsVector3s(int bitmask)
        {
            return !foundations.ContainsKey(bitmask) ? 
                foundations.First().Value.Select(index => vertices[index]) : 
                foundations[bitmask].Select(index => vertices[index]);
        }
        
        static IEnumerable<Vector2> GetShapeAsVector2s(int bitmask)
        {
            return !foundations.ContainsKey(bitmask) ? 
                foundations.First().Value.Select(index => vertices[index].To2()) : 
                foundations[bitmask].Select(index => vertices[index].To2());
        }

        public static IEnumerable<T> GetShape<T>(int bitmask)
        {
            if (typeof(T) == typeof(Vector3))
                return (IEnumerable<T>)GetShapeAsVector3s(bitmask);
            if (typeof(T) == typeof(Vector2))
                return (IEnumerable<T>)GetShapeAsVector2s(bitmask);

            throw new Exception("Unsupported type");
        }
        
        public static readonly Dictionary<int, int[]> foundations = new()
        {
            {
                0b00000000, new[]{5, 8, 11, 14}
            },
            {
                0b00000001, new[]{5, 9, 10, 14}
            },
            {
                0b00000010, new[]{6, 7, 11, 14}
            },
            {
                0b00000011, new[]{6, 7, 8, 9, 10, 14}
            },
            {
                0b00000100, new[]{5, 8, 12, 13}
            },
            {
                0b00000101, new[]{5, 9, 10, 11, 12, 13}
            },
            {
                0b00000110, new[]{6, 7, 12, 13}
            },
            {
                0b00000111, new[]{6, 7, 8, 9, 10, 11, 12, 13}
            },
            {
                0b00001000, new[]{4, 8, 11, 15}
            },
            {
                0b00001001, new[]{4, 9, 10, 15}
            },
            {
                0b00001010, new[]{4, 5, 6, 7, 11, 15}
            },
            {
                0b00001011, new[]{4, 5, 6, 7, 8, 9, 10, 15}
            },
            {
                0b00001100, new[]{4, 8, 12, 13, 14, 15}
            },
            {
                0b00001101, new[]{4, 9, 10, 11, 12, 13, 14, 15}
            },
            {
                0b00001110, new[]{4, 5, 6, 7, 12, 13, 14, 15}
            },
            {
                0b00001111, new[]{4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}
            },
            {
                0b00010011, new[]{6, 1, 10, 14}
            },
            {
                0b00010111, new[]{6, 1, 10, 11, 12, 13}
            },
            {
                0b00011011, new[]{4, 5, 6, 1, 10, 15}
            },
            {
                0b00011111, new[]{4, 5, 6, 1, 10, 11, 12, 13, 14, 15}
            },
            { 
                0b00100101, new[]{5, 9, 2, 13}
            },
            {
                0b00100111, new[]{6, 7, 8, 9, 2, 13}
            },
            {
                0b00101101, new[]{4, 9, 2, 13, 14, 15}
            },
            {
                0b00101111, new[]{4, 5, 6, 7, 8, 9, 2, 13, 14, 15}
            },
            {
                0b00110111, new[]{6, 1, 2, 13}
            },
            {
                0b00111111, new[]{4, 5, 6, 1, 2, 13, 14, 15}
            },
            {
                0b01001010, new[]{0, 7, 11, 15}
            },
            {
                0b01001011, new[]{0, 7, 8, 9, 10, 15}
            },
            {
                0b01001110, new[]{0, 7, 12, 13, 14, 15}
            },
            {
                0b01001111, new[]{0, 7, 8, 9, 10, 11, 12, 13, 14, 15}
            },
            {
                0b01011011, new[]{0, 1, 10, 15}
            },
            {
                0b01011111, new[]{0, 1, 10, 11, 12, 13, 14, 15}
            },
            {
                0b01101111, new[]{0, 7, 8, 9, 2, 13, 14, 15}
            },
            {
                0b01111111, new[]{0, 1, 2, 13, 14, 15}
            },
            {
                0b10001100, new[]{4, 8, 12, 3}
            },
            {
                0b10001101, new[]{4, 9, 10, 11, 12, 3}
            },
            {
                0b10001110, new[]{4, 5, 6, 7, 12, 3}
            },
            {
                0b10001111, new[]{4, 5, 6, 7, 8, 9, 10, 11, 12, 3}
            },
            {
                0b10011111, new[]{4, 5, 6, 1, 10, 11, 12, 3}
            },
            {
                0b10101101, new[]{4, 9, 2, 3}
            },
            {
                0b10101111, new[]{4, 5, 6, 7, 8, 9, 2, 3}
            },
            {
                0b10111111, new[]{4, 5, 6, 1, 2, 3}
            },
            {
                0b11001110, new[]{0, 7, 12, 3}
            },
            {
                0b11001111, new[]{0, 7, 8, 9, 10, 11, 12, 3}
            },
            {
                0b11011111, new[]{0, 1, 10, 11, 12, 3}
            },
            {
                0b11101111, new[]{0, 7, 8, 9, 2, 3}
            },
            {
                0b11111111, new[]{0, 1, 2, 3}
            }
        };

        /*public static readonly Dictionary<int, Vertex[]> Foundations = new()
        {
            {
                0b00000000, new[]{vertices[5], vertices[8], vertices[11], vertices[14]}
            },
            {
                0b00000001, new[]{vertices[5], vertices[9], vertices[10], vertices[14]}
            },
            {
                0b00000010, new[]{vertices[6], vertices[7], vertices[11], vertices[14]}
            },
            {
                0b00000011, new[]{vertices[6], vertices[7], vertices[8], vertices[9], vertices[10], vertices[14]}
            },
            {
                0b00000100, new[]{vertices[5], vertices[8], vertices[12], vertices[13]}
            },
            {
                0b00000101, new[]{vertices[5], vertices[9], vertices[10], vertices[11], vertices[12], vertices[13]}
            },
            {
                0b00000110, new[]{vertices[6], vertices[7], vertices[12], vertices[13]}
            },
            {
                0b00000111, new[]{vertices[6], vertices[7], vertices[8], vertices[9], vertices[10], vertices[11], vertices[12], vertices[13]}
            },
            {
                0b00001000, new[]{vertices[4], vertices[8], vertices[11], vertices[15]}
            },
            {
                0b00001001, new[]{vertices[4], vertices[9], vertices[10], vertices[15]}
            },
            {
                0b00001010, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[11], vertices[15]}
            },
            {
                0b00001011, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[8], vertices[9], vertices[10], vertices[15]}
            },
            {
                0b00001100, new[]{vertices[4], vertices[8], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00001101, new[]{vertices[4], vertices[9], vertices[10], vertices[11], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00001110, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00001111, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[8], vertices[9], vertices[10], vertices[11], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00010011, new[]{vertices[6], vertices[1], vertices[10], vertices[14]}
            },
            {
                0b00010111, new[]{vertices[6], vertices[1], vertices[10], vertices[11], vertices[12], vertices[13]}
            },
            {
                0b00011011, new[]{vertices[4], vertices[5], vertices[6], vertices[1], vertices[10], vertices[15]}
            },
            {
                0b00011111, new[]{vertices[4], vertices[5], vertices[6], vertices[1], vertices[10], vertices[11], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            { 
                0b00100101, new[]{vertices[5], vertices[9], vertices[2], vertices[13]}
            },
            {
                0b00100111, new[]{vertices[6], vertices[7], vertices[8], vertices[9], vertices[2], vertices[13]}
            },
            {
                0b00101101, new[]{vertices[4], vertices[9], vertices[2], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00101111, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[8], vertices[9], vertices[2], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b00110111, new[]{vertices[6], vertices[1], vertices[2], vertices[13]}
            },
            {
                0b00111111, new[]{vertices[4], vertices[5], vertices[6], vertices[1], vertices[2], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b01001010, new[]{vertices[0], vertices[7], vertices[11], vertices[15]}
            },
            {
                0b01001011, new[]{vertices[0], vertices[7], vertices[8], vertices[9], vertices[10], vertices[15]}
            },
            {
                0b01001110, new[]{vertices[0], vertices[7], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b01001111, new[]{vertices[0], vertices[7], vertices[8], vertices[9], vertices[10], vertices[11], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b01011011, new[]{vertices[0], vertices[1], vertices[10], vertices[15]}
            },
            {
                0b01011111, new[]{vertices[0], vertices[1], vertices[10], vertices[11], vertices[12], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b01101111, new[]{vertices[0], vertices[7], vertices[8], vertices[9], vertices[2], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b01111111, new[]{vertices[0], vertices[1], vertices[2], vertices[13], vertices[14], vertices[15]}
            },
            {
                0b10001100, new[]{vertices[4], vertices[8], vertices[12], vertices[3]}
            },
            {
                0b10001101, new[]{vertices[4], vertices[9], vertices[10], vertices[11], vertices[12], vertices[3]}
            },
            {
                0b10001110, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[12], vertices[3]}
            },
            {
                0b10001111, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[8], vertices[9], vertices[10], vertices[11], vertices[12], vertices[3]}
            },
            {
                0b10011111, new[]{vertices[4], vertices[5], vertices[6], vertices[1], vertices[10], vertices[11], vertices[12], vertices[3]}
            },
            {
                0b10101101, new[]{vertices[4], vertices[9], vertices[2], vertices[3]}
            },
            {
                0b10101111, new[]{vertices[4], vertices[5], vertices[6], vertices[7], vertices[8], vertices[9], vertices[2], vertices[3]}
            },
            {
                0b10111111, new[]{vertices[4], vertices[5], vertices[6], vertices[1], vertices[2], vertices[3]}
            },
            {
                0b11001110, new[]{vertices[0], vertices[7], vertices[12], vertices[3]}
            },
            {
                0b11001111, new[]{vertices[0], vertices[7], vertices[8], vertices[9], vertices[10], vertices[11], vertices[12], vertices[3]}
            },
            {
                0b11011111, new[]{vertices[0], vertices[1], vertices[10], vertices[11], vertices[12], vertices[3]}
            },
            {
                0b11101111, new[]{vertices[0], vertices[7], vertices[8], vertices[9], vertices[2], vertices[3]}
            },
            {
                0b11111111, new[]{vertices[0], vertices[1], vertices[2], vertices[3]}
            }
        };*/
    }
}