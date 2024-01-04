using System.Collections;
using Actions_Stuff;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace WIP
{
    public class AbilityHandler
    {
        Actor _actor;
        Ability _ability;

        Coroutine coroutine;

        public AbilityHandler(Actor actor, Ability ability)
        {
            _actor = actor;
            _ability = ability;
        }

        public int ID => _ability.id;
        public string AbilityName => _ability.abilityName;
        public string Description => _ability.description;

        public void Play()
        {
            //coroutine = _actor.StartCoroutine(_ability.behaviour.Execute());
        }

        /*public void Pause()
        {
            _ability.behaviour.Pause();
        }

        public void Resume()
        {
            _ability.behaviour.Resume();
        }
*/
        public void Stop()
        {
        }
    }
}