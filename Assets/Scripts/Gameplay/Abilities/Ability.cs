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
        public string abilityName;
        public List<Ability> _blockers = new();
        
        protected Raiser _raiser;
        protected Character _character;
        protected Coroutine _routine;

        void Awake() => _character = GetComponent<Character>();

        public void Hook() => _raiser.action += Execute;
        public void Unhook() => _raiser.action -= Execute;

        protected void SetRaiser(Raiser raiser) => _raiser = raiser;
        

        public void AddBlocker(Ability blocker) => _blockers.Add(blocker);
        public virtual void Init() => Debug.Log("Initialization method not implemented");
        protected virtual void Execute() => Debug.Log("Execute method not implemented");

        protected virtual void Stop() => IsActive = false;
        
        public bool IsActive { get; protected set; }

        protected bool IsBlocked => _blockers.Any(b => b.IsActive);
    }
}