using UnityEngine;

namespace Utils
{
    public static class Palette
    {
        public static Color[] colors { get; private set; }

        public static void Init()
        {
            SetupColors();
        }

        public static void SetupColors()
        {
            colors = new Color[City.map.granularity];
            for (var i = 0; i < colors.Length; i++)
                colors[i] = GetRandomColor();
        }
        static Color GetRandomColor()
        {
            return Color.HSVToRGB(Random.value, 1, Random.value);
        }
    }
}