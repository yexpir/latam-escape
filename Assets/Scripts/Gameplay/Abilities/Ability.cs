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
        public List<Ability> _blockers = new();
        
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
        public virtual void Init() => Debug.Log("Initialization method not implemented");
        protected virtual void Execute() => Debug.Log("Execute method not implemented");

        protected virtual void Stop() => IsActive = false;
        
        public bool IsActive { get; protected set; }

        protected bool IsBlocked => _blockers.Any(b => b.IsActive);
    }
}