using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using WIP.Utils;

namespace City
{
    public class Map
    {
        public Grid Grid { get; private set; }
        float _grain;

        public Map(float cellSize)
        {
            Debug.Log(cellSize);
            Grid = new Grid(cellSize);
        }

        public readonly Vector3[] Neighbours =
        {
            Vector3.forward,
            Vector3.forward + Vector3.right,
            Vector3.right,
            Vector3.right + Vector3.back,
            Vector3.back,
            Vector3.back + Vector3.left,
            Vector3.left,
            Vector3.forward + Vector3.left
        };

        /*public byte CheckNeighbours(Vector3 position, Grid grid)
        {
            byte neighbours = 0b00000000;
            var cellPos = grid.RoundPosition(position);
            for (var i = 0; i < 8; i++)
            {
                var neighbourPos = cellPos + Neighbours[i];
                if (AreNeighbours(cellPos, neighbourPos))
                {
                    neighbours = SetBit(neighbours, i);
                }
            }
            return neighbours;
        }*/

        /*public bool AreNeighbours(Vector3 callA, Vector3 callB)
        {
            var aFloor = (int)(Mathf.Floor(GetCellNoise(callA) / _grain) * _grain);
            var bFloor = (int)(Mathf.Floor(GetCellNoise(callB) / _grain) * _grain);
            return aFloor == bFloor;
        }*/
        
        /*public float GetCellNoise(Vector3 pos)
        {
            return Grid.PerlinNoise(pos);
        }*/
        
        
        public bool ReadBitBool(byte b, int i) =>  (b & (1 << i)) != 0;
        public int ReadBitInt(byte b, int i) => (b & (1 << i)) != 0 ? 1 : 0;
        static byte SetBit(byte b, int i) => (byte)(b | (1 << i));
        
        public void SetGranularity(float granularity)
        {
            _grain = granularity;
        }

        public void SetGrid(Grid grid)
        {
            Grid = grid;
        }
    }
}