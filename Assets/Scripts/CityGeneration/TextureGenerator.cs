using CityGeneration.Data;
using UnityEngine;

public class TextureGenerator
{
    SO_Map _mapData;

    public TextureGenerator(SO_Map mapData)
    {
        _mapData = mapData;
    }
    
    public Texture2D GetCellTexture(int bitmask)
    {
        var size = _mapData.buildingSize + _mapData.totalStreetWidth;
        var texture = new Texture2D(size, size) {filterMode = FilterMode.Point};
        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                var color = ShouldPaint(x, y, bitmask) ? Color.red : Color.white;
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    bool ShouldPaint(int x, int y, int bitmask)
    {
        int[] lines = { _mapData.totalStreetWidth/2, _mapData.buildingSize + _mapData.totalStreetWidth/2};
        var cellX = GetCellIndex(x, lines);
        var cellY = GetCellIndex(y, lines);
        
        if (cellX == 1 && cellY == 1)
        {
            return true;
        }

        var bitIndex = cellX switch
        {
            0 when cellY == 0 => 6,
            1 when cellY == 0 => 3,
            2 when cellY == 0 => 7,
            0 when cellY == 1 => 1,
            2 when cellY == 1 => 2,
            0 when cellY == 2 => 4,
            1 when cellY == 2 => 0,
            2 when cellY == 2 => 5,
            _ => -1
        };

        if (bitIndex == -1) return false;
        
        return (bitmask & (1 << bitIndex)) != 0;
    }

    int GetCellIndex(int pos, int[] lines)
    {
        for (var i = 0; i < lines.Length; i++)
        {
            if (pos < lines[i])
            {
                return i;
            }
        }
        return lines.Length;
    }
}