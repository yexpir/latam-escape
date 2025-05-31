using System.Collections.Generic;
using UnityEngine;
using Utils;
using Grid = Utils.Grid;

namespace CityStuff.GenerationStuff
{
    public static class MapCalculator
    {
        public static void Init()
        {
            City.perlin.SetSeed();
            City.perlin.SetOrigin();
        }

        public static int GetCellBitmask(Vector2 cell)
        {
            var bitmask = 0;
            var perlinA = GetPerlinNoise(cell);
            for (var i = 0; i < 8; i++)
            {
                if (i > 3)
                    if ((bitmask & cornerBits[i-4]) != cornerBits[i-4])
                        continue;
            
                var newCell = Vector2.zero;
                newCell.x = cell.x + Offsets[i, 0];
                newCell.y = cell.y + Offsets[i, 1];
            
                var perlinB = GetPerlinNoise(newCell);
            
                if (ShouldConnectCells(perlinA, perlinB))
                    bitmask |= (1 << i);
            }
            return bitmask;
        }
    
        static bool ShouldConnectCells(float perlinA, float perlinB)
        {
            var segmentIndexA = GetSegmentIndex(perlinA);
            var segmentIndexB = GetSegmentIndex(perlinB);
        
            return segmentIndexA == segmentIndexB;
        }

        public static int GetSegmentIndex(float perlin)
        {
            var segmentLength = 1f / (City.map.granularity-1);
            return Mathf.FloorToInt(perlin / segmentLength);
        }
    
        public static float GetPerlinNoise(Vector2 cell)
        {
            var xCoord = cell.x / City.area.x * City.perlin.scale + City.perlin.origin.x;
            var yCoord = cell.y / City.area.y * City.perlin.scale + City.perlin.origin.y;
            return Mathf.Clamp(Mathf.PerlinNoise(xCoord, yCoord), 0, 1f);
        }

        static int[,] Offsets { get; } =
        {
            {0, 1}, {-1, 0}, {1, 0}, {0, -1},  //edges:   top, left, right, bottom
            {-1, 1}, {1, 1}, {-1, -1}, {1, -1} //corners: top-left, top-right, bottom-left, bottom-right
            //bitmask representation:  BR BL TR TL B R L T
            /*
            {-1, 1},  {0, 1},  {1, 1},
            {-1, 0},           {1, 0},
            {-1, -1}, {0, -1}, {1, -1}
        */
        };

        //Bitmasks to tell each corner which edges to check
        static int[] cornerBits { get;  } =
        {
            0b0011,
            0b0101,
            0b1010,
            0b1100
        };

        public static Vector3 CellToWorld(Vector2 cell)
        {
            var pos = Vector3.zero;
            pos.x = cell.x * ConvertValue;
            pos.z = cell.y * ConvertValue;
            return Grid.Round(pos);
        }
        public static Vector2Int WorldToCell(Vector3 position)
        {
            var cell = Vector2Int.zero;
            var pos = Grid.Round(position);
            cell.x = Mathf.FloorToInt(pos.x / ConvertValue);
            cell.y = Mathf.FloorToInt(pos.z / ConvertValue);
            return cell;
        }
        
        public static int GetHeight(Vector2Int cell)
        {
            var noise = GetPerlinNoise(cell);
            return GetSegmentIndex(noise);
        }

        static List<int> combinaciones = new();
        static void GenerateCombination() // algorithm for generating all possible bitmasks -> use in case of losing foundation dictionary XD
        {
            for (var i = 0; i < 1 << 8; i++)
            {
                var isValid = true;
                for (var j = 0; j < 4; j++)
                {
                    if ((i & rules[j, 0]) != 0 && (i & rules[j, 1]) != rules[j, 1])
                    {
                        isValid = false;
                        break;
                    }
                }
                if(!isValid) continue;
                combinaciones.Add(i);
            }
        }

        static int[,] rules =
        {
            {0b00010000, 0b00000011},
            {0b00100000, 0b00000101},
            {0b01000000, 0b00001010},
            {0b10000000, 0b00001100},
        };

        public static float ConvertValue => City.map.blockSize * City.map.cellSize;
    }
}
