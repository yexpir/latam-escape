using UnityEngine;

namespace WIP.Utils
{
    public static class Grid
    {	
        static float CellSize => CityBuilder.cellSize;
        static int BlockSize => (int)(CityBuilder.blockUnit + CityBuilder.streetWidth);

        public static Vector3 RoundtPosition(Vector3 position)
        {
            position.x = Mathf.Round(position.x / CellSize) * CellSize;
            position.z = Mathf.Round(position.z / CellSize) * CellSize;
            return position;
        }

        static bool alternateStreet;
        public static Vector3 GetClosestStreetPosition(Vector3 pos)
        {
            if (alternateStreet)
            {
                pos.x = Mathf.Round(pos.x / BlockSize) * BlockSize + RandomLane();
                pos.z = Mathf.Round(pos.z / CellSize) * CellSize;
            }
            else
            {
                pos.x = Mathf.Round(pos.x / CellSize) * CellSize;
                pos.z = Mathf.Round(pos.z / BlockSize) * BlockSize + RandomLane();
            }
            alternateStreet = !alternateStreet;
            return pos;
        }

        static float RandomLane()
        {
            return (Random.Range(0, 3) - 1) * CityBuilder.cellSize;
        }

        public static Vector3 GetNextPosition(Vector3 position, Vector3 direction)
        {
            var nextPosition = position + direction.normalized * CellSize;
            return RoundToPoint(nextPosition, position);
        }

        public static Vector3 NextPosition(this Transform obj)
        {
            return GetNextPosition(obj.position, obj.forward);
        }

        public static Vector3 NthPosition(this Transform t, int n)
        {
            return GetNextNthPosition(t.position, t.forward, n);
        }

        public static Vector3 GetNextNthPosition(Vector3 currentPosition, Vector3 forwardDirection, int n)
        {
            var nextNth = currentPosition;
            for (var i = 0; i < n; i++)
                nextNth = GetNextPosition(nextNth, forwardDirection);
            return nextNth;
        }

        public static bool HasPassedPosition(Transform movingTransform, Vector3 position)
        {
            return Vector3.Dot(position - movingTransform.position, movingTransform.forward) < 0;
        }
        
        public static Vector3 RoundToPoint(Vector3 position, Vector3 point)
        {
            var nextPosition = position;

            if (position.x > point.x)
                nextPosition.x = Mathf.Floor(position.x / CellSize) * CellSize;
            else
                nextPosition.x = Mathf.Ceil(position.x / CellSize) * CellSize;

            if (position.z > point.z)
                nextPosition.z = Mathf.Floor(position.z / CellSize) * CellSize;
            else
                nextPosition.z = Mathf.Ceil(position.z / CellSize) * CellSize;

            return nextPosition;
        }
    }
}