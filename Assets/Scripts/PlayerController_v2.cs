using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Traveler))]
public class PlayerController_v2 : MonoBehaviour
{
    [SerializeField] PathBuilder _pathBuilderPrefab;
    [SerializeField] float _travelSpeed;
    float _sidestepSize;
    [SerializeField] float _sidestepSpeed;
    PathBuilder _pathBuilder;
    Traveler traveler;
    Path path;
    bool _isSidesepping;

    void Awake()
    {
        traveler = GetComponent<Traveler>();
        _pathBuilder = Instantiate(_pathBuilderPrefab);
        path = _pathBuilder.PathForward(transform,1000);
        _sidestepSize = CityBuilder.streetWidth / 3;
    }

    void Start()
    {
        traveler.Travel(path, _travelSpeed);
    }

    void Update()
    {
        if (_isSidesepping) return;
        
        if (In.RightPressed)
            Sidestep(1);
        if(In.LeftPressed)
            Sidestep(-1);
    }

    void Sidestep(float dir)
    {
        StartCoroutine(Co_Sidestep(dir));
    }

    IEnumerator Co_Sidestep(float dir)
    {
        _isSidesepping = true;
        
        var origin = path.transform.position;
        var target = origin + transform.right * (_sidestepSize * dir); 
        var time = 0.0f;
        while (time < 1)
        {
            path.transform.position = Vector3.Lerp(origin, target, time);
            print(time);
            time += Time.deltaTime * _sidestepSpeed;
            yield return null;
        }
        path.transform.position = Vector3.Lerp(origin, target, 1);

        _isSidesepping = false;
    }
}