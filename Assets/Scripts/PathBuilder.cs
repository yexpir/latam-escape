using System;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
class PathBuilder : MonoBehaviour
{
    Path _path;
    public Path Path => _path;
    SplineContainer _container;

    void Awake()
    {
        _container = GetComponent<SplineContainer>();
    }

    public Path PathForward(Transform origin, float maxDistance)
    {
        _path = new ForwardPath(_container, origin, maxDistance);
        return _path;
    }

    public Path CirclePath(Transform origin, float maxDistance)
    {
        _path = new CirclePath(_container, origin, maxDistance);
        return _path;
    }
}