using CityGeneration;
using CityGeneration.Data;
using UnityEngine;

namespace Gameplay.Utils
{
    public static class City
    {
        public static SO_City city { get; private set; }
        
        public static SO_Map map => city.mapData;
        public static SO_Perlin perlin => city.perlinData;
        public static Block blockPrefab => city.blockPrefab;
        public static Vector2 area => city.area;

        public static void SetCity(SO_City city)
        {
            City.city = city;
        }
    }
}