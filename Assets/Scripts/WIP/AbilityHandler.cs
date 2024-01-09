using System;
using System.Collections;
using Actions_Stuff;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace WIP
{
    [Serializable]
    public class AbilityHandler
    {
        Actor _actor;
        public Ability _ability;
        
        public Coroutine coroutine;

        public AbilityState state;
        public bool IsActive => state == AbilityState.Active;
        public int ID => _ability.id;
        public string AbilityName => _ability.abilityName;
        public string Description => _ability.description;

        public AbilityHandler(Actor actor, Ability ability)
        {
            _actor = actor;
            ability.behaviour.SetActor(_actor);
            _ability = ability;
            state = AbilityState.Inactive;
        }

        public void SetActor(Actor actor) => _actor = actor;
        public void SetAbility(Ability ability) => _ability = ability;
        

        public void Play()
        {
            _ability.behaviour.Reset();
            coroutine = _actor.StartCoroutine(_ability.behaviour.Execute());
            _actor.AddActiveAction(this);
            state = AbilityState.Active;
        }

        public void Pause()
        {
            _ability.behaviour.Pause();
            state = AbilityState.Waiting;
        }

        public void Resume()
        {
            _ability.behaviour.Resume();
            state = AbilityState.Active;
        }
        public void Stop()
        {
            //_actor.StopCoroutine(coroutine);
            _ability.behaviour.Stop();
            coroutine = null;
            _actor.RemoveActiveAction(this);
            state = AbilityState.Inactive;
        }
    }
    public enum AbilityState{
        Inactive,
        Active,        
        Waiting
    }
}