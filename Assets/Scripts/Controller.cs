using System;
using Actions_Stuff;
using UnityEngine;
using WIP;

public class Controller : MonoBehaviour
{
    [SerializeField] Character _data;
    Actor _actor;
    Traveler _traveler;

    void Awake()
    {
        _actor = GetComponent<Actor>();
        var path = PathBuilder.Instance.PathForward(transform, 1000);
        var actionRoutine = new ActionRoutine();
        actionRoutine.SetID((int)Actions.Forward);
        actionRoutine.SetEnumerator(_traveler.Co_Travel(path, _data.travelSpeed));
        _actor.AddAction(actionRoutine);
    }
}