using System.Collections;
using Unity.VisualScripting.FullSerializer;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace City
{
    public class Building
    {
        Map _map;
        Dictionary<string, GameObject> _tilesPool = new(); // 000, 010, 100, 110, 111
        GameObject[] _tilesToRender = new GameObject[4];

        /*public Building(Map map, GameObject[] tilesPool)
        {
            _map = map;
            InitPool(tilesPool);
        }*/

        

        /*public GameObject[] GetTilesToRender(Vector3 cellPos)
        {
            CalculateTiles(cellPos);
            return _tilesToRender;
        }*/

        /*public void CalculateTiles(Vector3 cellPos)
        {
            var neighbours = _map.CheckNeighbours(cellPos, _map.Grid);
            for (var i = 0; i < 4; i++)
            {
                var tileBits = "";
                var i2 = i * 2;
                for (var j = 0; j < 3; j++)
                {
                    var indexFixed = (i2+1) % 8 - 1 + j;
                    tileBits += _map.ReadBitInt(neighbours, indexFixed);
                }

                if (_tilesPool.ContainsKey(tileBits))
                {
                    _tilesToRender[i] = _tilesPool[tileBits];
                }
            }
        }*/

        public void InitPool(GameObject[] tiles)
        {
            _tilesPool.Add("000", tiles[0]);
            _tilesPool.Add("010", tiles[1]);
            _tilesPool.Add("100", tiles[2]);
            _tilesPool.Add("110", tiles[3]);
            _tilesPool.Add("111", tiles[4]);
        }
    }
}