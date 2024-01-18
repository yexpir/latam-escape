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

        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }
        public bool IsBlocked { get; private set; }
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
                if(a != null) a.Stop();
            }
        }

        public void SetActor(Actor actor) => _actor = actor;
        
        public abstract IEnumerator Execute();
        public void Pause() => IsPaused = true;
        public void Resume() => IsPaused = false;
        public void Stop() => IsStopped = true;
        public void Reset()
        {
            IsStopped = false;
            IsPaused = false;
        }

        public void Block(){
            IsBlocked = true;
        }
        public void UnBlock(){
            IsBlocked = false;
        }
    }
}
