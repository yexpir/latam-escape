using System;
using CityGeneration.Data;
using UnityEngine;

namespace CityGeneration
{
    public class PerlinDisplay : MonoBehaviour
    {
        [SerializeField] SO_City _city;

        MeshRenderer _renderer;

        void OnEnable()
        {
            _city.OnCityValidated += Init;
        }

        void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        void Start()
        {
            Init();
        }

        void Update()
        {
            _renderer.material.mainTexture = GenerateTexture();
        }

        public void Init()
        {
            transform.position = new Vector3(ShapeData.B + MapCalculator.ConvertValue * _city.area.x, 0, ShapeData.B);
            transform.localScale = new Vector3(MapCalculator.ConvertValue * _city.area.x / 10, 1, MapCalculator.ConvertValue * _city.area.y / 10);
        }

        Texture2D GenerateTexture()
        {
            var width = (int)_city.area.x;
            var height = (int)_city.area.y;
            
            var offset = _city.area/2;
            var endPoint = CityBuilder.PlayerCell - offset;
            
            var texture = new Texture2D(width, height);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = CalculateColor(x+(int)endPoint.x+1, y+(int)endPoint.y);
                    texture.SetPixel(width - x - 1, height - y - 1, color);
                }
            }
            texture.filterMode = FilterMode.Point;
            texture.Apply();
            return texture;
        }

        static Color CalculateColor(int x, int y)
        {
            // var xCoord = x / _city.area.x * _city.perlinData.scale + _city.perlinData.origin.x;
            // var yCoord = y / _city.area.y * _city.perlinData.scale + _city.perlinData.origin.y;
            var vector = new Vector2(x, y);
            var sample = MapCalculator.GetPerlinNoise(vector);
            return new Color(sample, sample, sample);
        }
    }
}
