using UnityEngine;

public class Zone
{
    public bool hasBeenVisited;

    Vector3 _origin;
    Vector3 _center;
    readonly int _width;
    readonly int _height;

    public Zone(Vector3 origin, int width, int height)
    {
        var cellSize = (int)CityBuilder.cellSize;
        _origin = origin;
        _width = width * cellSize;
        _height = height * cellSize;
        _center = origin + Vector3.right * _width + Vector3.forward * _height;
    }

    public Vector3 Origin => _origin;
    public Vector3 Center => _center;
    public int Width => _width;
    public int Height => _height;
}