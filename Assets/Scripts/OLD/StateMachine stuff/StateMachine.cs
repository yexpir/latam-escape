using System.Collections;
using StateMachine_stuff.States;
using States;
using UnityEngine;


public class StateMachine
{
    [SerializeField] Character playerData;
    TravelerOld _actor;
    BaseState _currentState;
    
    public StateMachine(TravelerOld actor)
    {
        this._actor = actor;
    }

    public IEnumerator StartMachine()
    {
        yield return _actor.StartCoroutine(ChangeState(new ForwardState()));
    }

    public IEnumerator ChangeState(BaseState newState)
    {
        if(_currentState != null)
            yield return _actor.StartCoroutine(_currentState.Exit(_actor));
        _currentState = newState;
        yield return _actor.StartCoroutine(_currentState.Enter(_actor));
        _actor.StartCoroutine(_currentState.Execute(_actor));
    }
}