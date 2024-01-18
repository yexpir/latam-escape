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
            Debug.Log("PLAY");
            Actor.StartCoroutine(HandleAbilityInteractions());
            if(Ability.behaviour.IsBlocked) return;
            Ability.behaviour.Reset();
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
            var blocker = ActiveBlockers.FirstOrDefault();
            if(blocker != null)
            {
                if(blocker != this)
                {
                    Stop();
                    Ability.behaviour.Block();
                    Debug.Log("BLOCKING DIFFERENT");
                    yield break;
                }
                else
                {
                    Debug.Log("BLOCKING SELF");
                    Ability.behaviour.Block();
                    yield break;
                }
            }
            else
            {
                Ability.behaviour.UnBlock();
            }

            while(ActiveBufferers.Any())
            {
                Pause();
                yield return null;
            }
            Resume();
            
            foreach(AbilityHandler cancellable in ActiveCancellables)
            {
                cancellable.Stop();
            }
            foreach(AbilityHandler interruptable in ActiveInterruptables)
            {
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