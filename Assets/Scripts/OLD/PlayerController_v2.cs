using System.Collections;
using Actions_Stuff;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(TravelerOld))]
public class PlayerController_v2 : MonoBehaviour
{
    [SerializeField] Character _data;
    TravelerOld _travelerOld;
    PathBuilder _pathBuilder;
    ActionRoutine _currentRoutine;

    public void GoForward()
    {
        var path = _pathBuilder.PathForward(transform, 1000);
        //_currentRoutine = new ActionRoutine("Forward",_traveler.Travel(path, _data.travelSpeed));
    }

    public IEnumerator SideStep(float dir)
    {
        if(_currentRoutine.id.Equals("Turn"))
            yield return _currentRoutine;
        //_currentRoutine = new ActionRoutine("Sidestep",_traveler.Sidestep(CityBuilder.stepsideSize, _data.sidestepSpeed, dir));
    }

    public IEnumerator Turn()
    {
        _travelerOld.Exit();
        yield return _currentRoutine;
        _travelerOld.Enter();
    }
}