using UnityEngine;

namespace WIP.Utils
{
    public class Perlin
    {
        float _scale = 1f;
        float _size= 1f;

        public void SetScale(float scale)
        {
            _scale = scale;
        }
        public void SetSize(float size)
        {
            _size = size;
        }

        public float GetValueRaw(float x, float y)
        {
            return Mathf.PerlinNoise(x, y);
        }
        
        public float GetValue(float x, float y, float size, float scale)
        {
            var mult = scale / size;
            var xCoord = x / size * scale;
            var yCoord = y / size * scale;
            return Mathf.PerlinNoise(xCoord, yCoord);
        }
        
        public float GetValueBaseTen(float x, float y)
        {
            return GetValue(x, y, 1, 1) * 10;
        }
    }
}