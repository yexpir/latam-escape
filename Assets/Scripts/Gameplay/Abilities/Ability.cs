using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.Abilities
{
    [RequireComponent(typeof(Character))]
    [Serializable]
    public abstract class Ability : MonoBehaviour
    {
        [HideInInspector]public string abilityName;
        public List<Ability> _blockers = new();//Stop you from starting
        public List<Ability> _stoppables = new();//Stop when you start
        
        protected Character _character;
        protected Raiser _raiser;
        protected Coroutine _routine;

        protected virtual void Awake()
        {
            _character = GetComponent<Character>();
            abilityName = name;
        }

        public void Hook()
        {
            if(_raiser == null) return;
                _raiser.action += Execute;
        }

        public void Unhook()
        {
            if(_raiser == null) return;
            _raiser.action -= Execute;
        }

        protected void SetRaiser(Raiser raiser) => _raiser = raiser;
        public void AddBlocker(Ability blocker) => _blockers.Add(blocker);
        public virtual void Init() => Debug.LogWarning("Initialization method not implemented");
        protected virtual void Execute() => Debug.LogWarning("Execute method not implemented");

        public virtual void Stop()
        {
            IsActive = false;
            IsStopped = true;
        }

        public bool IsActive { get; protected set; }

        protected bool IsBlocked => _blockers.Any(b => b.IsActive);
        protected bool IsStopped;

        protected void StopStoppables()
        {
            foreach (var stoppable in _stoppables)
            {
                print($"{stoppable.abilityName} stopped");
                stoppable.Stop();
            }
        }

        public void Activate()
        {
            IsActive = true;
            StopStoppables();
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}