using UnityEngine;
using WIP.Utils;

namespace City
{
    public class Grid
    {
        public Grid(float cellSize)
        {
            CellSize = cellSize;
        }

        public float CellSize { get; private set; }

        public void SetCellSize(float cellSize) => CellSize = cellSize;

        public Vector3 RoundPosition(Vector3 position)
        {
            return GridUtil.RoundPosition(position, CellSize);
        }
        
        public Vector3 GetNextPosition(Vector3 position, Vector3 direction)
        {
            var nextPosition = position + direction.normalized * CellSize;
            return GridUtil.RoundToPoint(nextPosition, position);
        }

        /*public float PerlinNoise(Vector3 pos)
        {
            var cell = RoundPosition(pos);
            return Perlin.GetValueBaseTen(cell.x, cell.z); 
        }*/
    }
}