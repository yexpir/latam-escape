using CityGeneration;
using UnityEngine;

namespace Gameplay.Utils
{
    public static class Grid
    {
        public static readonly float CellSize = City.map.cellSize;
        public static readonly float CellRadius = CellSize / 2;

        public static Vector3 GetNextPosition(Vector3 position, Vector3 direction)
        {
            var nextPosition = position + direction.normalized * CellSize;
            return RoundToPoint(nextPosition, position);
        }
        public static Vector3 GetNextPosition(Vector3 position, Vector3 direction, float cellSize)
        {
            var nextPosition = position + direction.normalized * cellSize;
            return RoundToPoint(nextPosition, position);
        }
        
        public static Vector3 GetNthPosition(Vector3 position, Vector3 direction, int n) //cardinal direction
        {
            var nextPosition = position + direction.normalized * (CellSize * n);
            return RoundToPoint(nextPosition, position);
        }
        
        public static Vector3 NextPosition(this Transform obj)
        {
            return GetNextPosition(obj.position, obj.forward);
        }

        public static Vector3 NthPosition(this Transform t, int n)
        {
            return GetNthPosition(t.position, t.forward, n);
        }
        
        public static bool HasPassedPosition(Transform movingTransform, Vector3 position)
        {
            return Vector3.Dot(position - movingTransform.position, movingTransform.forward) < 0;
        }

        public static Vector3 Round(Vector3 position)
        {
            position.x = Mathf.Round(position.x / CellSize) * CellSize;
            position.z = Mathf.Round(position.z / CellSize) * CellSize;
            return position;
        }

        public static Vector3 Round(Vector3 position, float cellSize)
        {
            position.x = Mathf.Round(position.x / cellSize) * cellSize;
            position.z = Mathf.Round(position.z / cellSize) * cellSize;
            return position;
        }
        
        public static Vector3 RoundToPoint(Vector3 position, Vector3 point)
        {
            var nextPosition = position;
            
            var auxX = position.x / CellSize;
            var auxZ = position.z / CellSize;

            nextPosition.x = (position.x > point.x ? Mathf.Floor(auxX) : Mathf.Ceil(auxX)) * CellSize;
            nextPosition.z = (position.z > point.z ? Mathf.Floor(auxZ) : Mathf.Ceil(auxZ)) * CellSize;

            return nextPosition;
        }

        public static Vector3 RoundToPoint(Vector3 position, Vector3 point, float cellSize)
        {
            var nextPosition = position;
            
            var auxX = position.x / cellSize;
            var auxZ = position.z / cellSize;

            nextPosition.x = (position.x > point.x ? Mathf.Floor(auxX) : Mathf.Ceil(auxX)) * cellSize;
            nextPosition.z = (position.z > point.z ? Mathf.Floor(auxZ) : Mathf.Ceil(auxZ)) * cellSize;

            return nextPosition;
        }

        public static bool IsCellEmpty(Vector3 position)
        {
            return Physics.OverlapSphereNonAlloc(Round(position), CellRadius, _results, Block.layerMask) == 0;
        }static Collider[] _results = new Collider[1];
    }
}