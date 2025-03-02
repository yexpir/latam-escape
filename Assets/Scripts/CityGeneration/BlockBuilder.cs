using System.Collections.Generic;
using CityGeneration.Data;
using Extensions;
using Gameplay.Utils;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CityGeneration
{
    public class BlockBuilder
    {
        Transform _parent;
        
        List<Color> _colors = new();

        public BlockBuilder()
        {
            SetupColors();
        }

        public void Init(Transform parent)
        {
            _parent = parent;
            SetupColors();
        }

        public Block BuildBlock(Vector2 cell)
        {
            var position = MapCalculator.CellToWorld(cell);
            var bitmask = MapCalculator.GetCellBitmask(cell);
            var mesh = MeshData.GetMesh(bitmask);
            var height = Mathf.RoundToInt(Mathf.Pow(MapCalculator.GetHeight(cell) + 1, City.map.heightPow) * City.map.heightMult);
            
            var block = Object.Instantiate(City.blockPrefab, position, quaternion.identity, _parent);
            block.Init(mesh, _colors[MapCalculator.GetHeight(cell)], height, bitmask.BitToString());
            
            return block;
        }

        void SetupColors()
        {
            _colors.Clear();
            for (var i = 0; i < City.map.granularity; i++)
                _colors.Add(GetRandomColor());
        }

        static Color GetColor(Vector2 cell)
        {
            var a = MapCalculator.GetPerlinNoise(cell);
            return new Color(a, a, a);
        }
        
        static Color GetRandomColor()
        {
            var r = Random.value;
            var g = Random.value;
            var b = Random.value;

            return new Color(r, g, b);
        }
    }
}