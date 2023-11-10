using System;
using UnityEngine;


[RequireComponent(typeof(Traveler))]
public class PlayerController_v2 : MonoBehaviour
{
    [SerializeField] PathBuilder _pathBuilderPrefab;
    [SerializeField] float _travelSpeed;
    PathBuilder _pathBuilder;
    Traveler traveler;
    Path path;

    void Awake()
    {
        traveler = GetComponent<Traveler>();
        _pathBuilder = Instantiate(_pathBuilderPrefab);
        path = _pathBuilder.PathForward(transform,1000);
    }

    void Start()
    {
        traveler.Travel(path, _travelSpeed);
    }
}