using CityStuff.ConfigurationStuff;
using CityStuff.PoolStuff.PrefabStuff;
using CityStuff.PrefabStuff;
using UnityEngine;

namespace Utils
{
    public static class City
    {
        public static SO_City city { get; private set; }
        
        public static SO_Map map => city.mapData;
        public static SO_Perlin perlin => city.perlinData;
        public static SO_Block block => city.blockData;
        public static SO_SpawnAreas spawnAreas => city.spawnAreas;
        public static SO_WorldObjectSet worldObjectSet => city.worldObjectSet;
        public static Block blockPrefab => city.blockPrefab;
        
        public static Vector2 area => city.area;

        public static void SetCity(SO_City city)
        {
            City.city = city;
        }
    }
}