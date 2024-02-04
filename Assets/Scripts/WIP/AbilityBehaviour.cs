using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using System.Linq;


namespace WIP
{
    public abstract class AbilityBehaviour : ScriptableObject
    {
        protected Actor _actor;

        public bool IsPaused { get; protected set; }
        public bool IsStopped { get; protected set; }
        public bool IsBlocked { get; protected set; }
        public  bool _hasEnded;
        public bool HasEnded
        { 
            get
            {
                return _hasEnded;
            } 
            protected set 
            {
                _hasEnded = value;
                if(!value) return;
                var a = _actor.ActiveAbilities.FirstOrDefault(a => a.Ability.behaviour == this);
                a?.Stop();
            }
        }

        public bool IsPausing{ get; protected set; }
        public bool IsStopping{ get; protected set; }
        public bool IsEnding { get; protected set; }

        public void SetActor(Actor actor) => _actor = actor;
        
        public abstract IEnumerator Execute();
        public virtual void Pause() => IsPaused = true;
        public virtual void Resume() => IsPaused = false;
        public virtual void Stop() => IsStopped = true;
        public void Reset()
        {
            IsStopped = false;
            IsPaused = false;
            IsBlocked = false;
            HasEnded = false;

            IsPausing = false;
            IsStopping = false;
            IsEnding = false;
        }

        public void Block(){
            IsBlocked = true;
        }
        public void UnBlock(){
            IsBlocked = false;
        }

        public virtual void RequestEnd()
        {
            IsEnding = true;
        }
    }
}
