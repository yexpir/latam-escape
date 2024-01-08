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
        Ability _ability;
        
        public Coroutine coroutine;

        public int ID => _ability.id;
        public string AbilityName => _ability.abilityName;
        public string Description => _ability.description;

        public AbilityHandler(Actor actor, Ability ability)
        {
            _actor = actor;
            _ability = ability;
            _ability.behaviour.SetActor(_actor);
        }

        public void SetActor(Actor actor) => _actor = actor;
        public void SetAbility(Ability ability) => _ability = ability;
        

        public void Play()
        {
            coroutine = _actor.StartCoroutine(_ability.behaviour.Execute());
            _actor.AddActiveAction(this);
        }

        public void Pause()
        {
            _ability.behaviour.Pause();
        }

        public void Resume()
        {
            _ability.behaviour.Resume();
        }
        public void Stop()
        {
            _actor.StopCoroutine(coroutine);
            coroutine = null;
            _actor.RemoveActiveAction(this);
        }
    }
}