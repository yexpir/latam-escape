using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using WIP.Utils;

namespace City
{
    public class CityBuilder2 : MonoBehaviour
    {
        Map _map;

        public float granularity;
        public float noiseScale;
        public float noiseSize;
        public GameObject[] buildingTiles;
        Building _building;

        /*void Awake()
        {
            _map = new Map(1.0f);
            // Perlin.SetScale(noiseScale);
            // Perlin.SetSize(noiseSize);
            _building = new Building(_map, buildingTiles);
        }*/

        void Start()
        {
            // BuildCity();
        }

        void Update()
        {
            // print(ByteToString(_map.CheckNeighbours(transform.position, _map.Grid)));
            // print(_map.Grid.RoundPosition(transform.position) +" --- "+ (int)_map.GetCellNoise(transform.position));
            transform.position = _map.Grid.RoundPosition(transform.position);
        }
        string ByteToString(byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }

        /*void BuildCity()
        {
            var cellPos = Vector3.zero;
            for (var i = 0; i < 20; i++)
            {
                cellPos.x = i;
                for (var j = 0; j < 20; j++)
                {
                    cellPos.z = j;
                    Build(cellPos);
                }
            }            
        }*/

        /*void Build(Vector3 cellPos)
        {
            var tiles = _building.GetTilesToRender(cellPos);
            for (var i = 0; i < 4; i++)
            {
                var tilePos = cellPos + _map.Neighbours[i*2+1]/2 * _map.Grid.CellSize;
                Instantiate(tiles[i], tilePos, Quaternion.identity);
            }
        }*/
        
        /*void OnValidate()
        {
            _map?.SetGranularity(granularity);
            Perlin.SetScale(noiseScale);
            Perlin.SetSize(noiseSize);
        }*/
        
    }
}
