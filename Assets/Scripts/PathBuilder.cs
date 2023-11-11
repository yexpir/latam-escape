using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

class PathBuilder : MonoBehaviour
{
    [SerializeField] ForwardPath _forwardPathPrefab;
    [SerializeField] CirclePath _circlePathPrefab;
    public Path Path => _path;
    Path _path;

    void Awake()
    {
    }

    public Path PathForward(Transform origin, float maxDistance)
    {
        _path = Instantiate(_forwardPathPrefab);
        ((ForwardPath)_path).Init(origin, maxDistance);
        return _path;
    }

    public Path CirclePath(Transform origin, float maxDistance)
    {
        _path = Instantiate(_circlePathPrefab);
        return _path;
    }
}