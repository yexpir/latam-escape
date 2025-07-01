using System.Collections.Generic;
using CityStuff.GenerationStuff;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using CityStuff.PrefabStuff;
using Extensions;
using Unity.Mathematics;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace CityStuff.PoolStuff.PrefabStuff
{
    public class BlockBuilder//NOT BEING USED
    {
        Transform _parent;
        
        List<Color> _colors = new();

        public BlockBuilder(Transform parent)
        {
            SetupColors();
            Init(parent);
        }

        public void Init(Transform parent)
        {
            _parent = parent;
            SetupColors();
        }

        public Block BuildBlock(Vector2Int cell)
        {
            var position = MapCalculator.CellToWorld(cell);
            var bitmask = MapCalculator.GetCellBitmask(cell);
            var mesh = MeshData.GetMesh(bitmask);
            var height = Mathf.RoundToInt(Mathf.Pow(MapCalculator.GetHeight(cell) + 1, City.map.heightPow) * City.map.heightMult);
            
            var block = Object.Instantiate(City.blockPrefab, position, quaternion.identity, _parent);
            block.Init(mesh, _colors[MapCalculator.GetHeight(cell)], height, bitmask.BitToString());
            
            return block;
        }

        public void GetBlock(int bitmask)
        {
            var position = Vector3.zero;
            var mesh = MeshData.GetMesh(bitmask);
            
            var block = Object.Instantiate(City.blockPrefab, position, quaternion.identity, _parent);
            block.Init(mesh, Color.magenta, 1, bitmask.BitToString());
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
        
        public static Color GetRandomColor()
        {
            return Color.HSVToRGB(Random.value, 1, Random.value);
        }
    }
}