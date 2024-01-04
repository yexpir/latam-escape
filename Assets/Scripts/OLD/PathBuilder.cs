using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] ForwardPath _forwardPathPrefab;
    [SerializeField] CirclePath _circlePathPrefab;
    public Path Path => _path;
    Path _path;

    public static PathBuilder Instance;

    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public ForwardPath PathForward(Transform origin, float maxDistance)
    {
        _path = Instantiate(_forwardPathPrefab);
        ((ForwardPath)_path).Init(origin, maxDistance);
        return (ForwardPath)_path;
    }

    public CirclePath CirclePath(Transform origin, int dir)
    {
        _path = Instantiate(_circlePathPrefab);
        ((CirclePath)_path).Init(origin, dir);
        return (CirclePath)_path;
    }
}