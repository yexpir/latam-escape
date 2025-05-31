using System.Collections.Generic;
using System.Linq;
using Extensions;
using UnityEngine;

namespace Utils
{
    public static class StreetService
    {
        public static readonly float[] lanes = new float[City.map.laneCount];

        public static void Init()
        {
            var start = -(City.map.totalLanesWidth-1) / 2.0f;
            var laneWidth = Grid.CellSize * City.map.laneWidth;
            for (var i = 0; i < lanes.Length; i++)
                lanes[i] = start + i * laneWidth;
        }
        
        public static float GetClosestStreetLaneInDirection(Vector3 position, Vector3 direction)
        {
            var target = position.Mult(direction.Round().Abs().Flatten().normalized).Max();
            var streetLanes = GetStreetLanes(GetClosestStreet(position, direction)).ToArray();
            var closestLanes = streetLanes.Where(n => direction.Max() < 0 ? n < target : n > target).OrderBy(n => Mathf.Abs(n - target)).ToArray();
            if (closestLanes.Any())
                return closestLanes.First();
            return closestLanes.Any() ? closestLanes.First() : direction.Max() < 0 ? streetLanes[0] : streetLanes[^1];
        }

        public static float GetClosestStreet(Vector3 position, Vector3 direction)
        {
            var axisValue = position.Mult(direction.Round().Abs().Flatten().normalized).Max();
            return Mathf.Round(axisValue / City.map.blockSize) * City.map.blockSize;
        }

        public static float GetClosestStreetInDirection(Vector3 position, Vector3 direction)
        {
            var axisValue = position.Mult(direction.Round().Abs().Flatten().normalized).Max();
            if (direction.Max() < 0)
                return Mathf.Floor(axisValue / City.map.blockSize) * City.map.blockSize;
            return Mathf.Ceil(axisValue / City.map.blockSize) * City.map.blockSize;
        }
        
        public static IEnumerable<float> GetClosestStreets(Vector3 position, Vector3 direction)
        {
            var dir = direction.Round().Flatten().normalized;
            var result = new[] {GetClosestStreetInDirection(position, dir), GetClosestStreetInDirection(position, -dir)};
            return result;
        }

        public static IEnumerable<float> GetStreetLanes(float street)
        {
            return lanes.Select(lane => street + lane); 
        }

        public static IEnumerable<float> GetClosestLanes(Vector3 position, Vector3 direction)
        {
            var result = new List<float>();
            var closestStreets = GetClosestStreets(position, direction);
            foreach (var street in closestStreets)
                result.AddRange(GetStreetLanes(street));
            return result;
        }

        public static Vector3 GetNextLaneInDirection(Vector3 position, Vector3 direction)
        {
            var pos = position.Mult(direction.Round().Abs()).Max();
            var dir = direction.Max();
            var lane = GetClosestLanes(position, direction).Where(n => dir < 0 ? n < pos : n > pos).OrderBy(n => n - pos).First();
            return Grid.Round(position.ProjectValueWithSelector(lane, direction));
        }

        public static Vector3 GetNextStreet(Vector3 position, Vector3 direction)
        {
            var street = GetClosestStreet(position, direction);
            var streetCell = position.ProjectValueWithSelector(street, direction);
            return Grid.Round(streetCell);
        }
            
        public static Vector3 GetNextStreetInDirection(Vector3 position, Vector3 direction)
        {
            var street = GetClosestStreetInDirection(position, direction);
            var streetCell = position.ProjectValueWithSelector(street, direction);
            return Grid.Round(streetCell);
        }

        public static Vector3 GetNextIntersection(Vector3 position, Vector3 forward, Vector3 side)
        {
            var currentStreet = GetClosestStreet(position, side);
            var crossStreet = GetClosestStreetInDirection(position, forward);
            var intersection = Vector3.zero.ProjectValueWithSelector(currentStreet, side);
            intersection = intersection.ProjectValueWithSelector(crossStreet, forward);
            return intersection;
        }

        public static IEnumerable<Vector3> LanesAsVectors(Vector3 selector)
        {
            return lanes.Select(l => Vector3.zero.ProjectValueWithSelector(l, selector.Round()));
        }
        
        
    }
}