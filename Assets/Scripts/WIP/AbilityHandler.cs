using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WIP
{
    [Serializable]
    public class AbilityHandler
    {
        public Actor Actor {get; private set;}
        [field:SerializeField]public Ability Ability {get; private set;}

        public string AbilityName => Ability.abilityName;
        public string Description => Ability.description;
        
        public Coroutine coroutine;

        [HideInInspector]public AbilityState state;
        public bool IsActive => state == AbilityState.Active;
        public int ID => Ability.id;

        public List<Ability> blockers = new();
        public List<Ability> bufferers = new();
        public List<Ability> cancellables = new();
        public List<Ability> interruptables = new();

        HashSet<AbilityHandler> ActiveBlockers => Actor.ActiveAbilities.Where(h => blockers.Any(a => h.Ability.Equals(a))).ToHashSet();
        HashSet<AbilityHandler> ActiveBufferers => Actor.ActiveAbilities.Where(h => bufferers.Any(a => h.Ability.Equals(a))).ToHashSet();
        HashSet<AbilityHandler> ActiveCancellables => Actor.ActiveAbilities.Where(h => cancellables.Any(a => h.Ability.Equals(a))).ToHashSet();
        HashSet<AbilityHandler> ActiveInterruptables => Actor.ActiveAbilities.Where(h => interruptables.Any(a => h.Ability.Equals(a))).ToHashSet();
        
        public void Init(Actor actor){
            state = AbilityState.Inactive;
            Actor = actor;
            Ability.behaviour.SetActor(Actor);
        }

        public void SetAbility(Ability ability) => Ability = ability;
        
        public void Play()
        {
            Ability.behaviour.Reset();
            Actor.StartCoroutine(HandleAbilityInteractions());
            coroutine = Actor.StartCoroutine(Ability.behaviour.Execute());
            Actor.AddActiveAction(this);
            state = AbilityState.Active;
        }

        public void Pause()
        {
            Ability.behaviour.Pause();
            state = AbilityState.Waiting;
        }

        public void Resume()
        {
            Ability.behaviour.Resume();
            state = AbilityState.Active;
        }
        public void Stop()
        {
            Ability.behaviour.Stop();
            coroutine = null;
            Actor.RemoveActiveAction(this);
            state = AbilityState.Inactive;
            ResumeInterruptables();
        }


        public IEnumerator HandleAbilityInteractions()
        {
            Debug.Log(AbilityName + " INTERACTION");
            if(ActiveBlockers.Any())
            {
                Debug.Log(AbilityName + " has active blockers");
                Stop();
                yield break;
            }

            while(ActiveBufferers.Any())
            {
                Debug.Log(AbilityName + " has active bufferers");
                Pause();
                yield return null;
            }
            Resume();
            
            foreach(AbilityHandler cancellable in ActiveCancellables)
            {
                Debug.Log(AbilityName + " cancells " + cancellable.AbilityName);
                cancellable.Stop();
            }
            foreach(AbilityHandler interruptable in ActiveInterruptables)
            {
                Debug.Log(AbilityName + " interrupts " + interruptable.AbilityName);
                interruptable.Pause();
            }
        }

        public void ResumeInterruptables()
        {
            foreach(AbilityHandler interruptable in ActiveInterruptables){
                if(interruptable.IsActive){
                    interruptable.Resume();
                }
            }
        }
    }
    public enum AbilityState{
        Inactive,
        Active,        
        Waiting
    }
}