using System;
using CityStuff.PrefabStuff;
using Extensions;
using UnityEngine;
using Gameplay.InputHandling;
using Gameplay.Abilities;
using Utils;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        public static readonly Raiser OnSideStep = new();
        public static readonly Raiser OnTurn = new();
        public static readonly Raiser OnRun = new();
        public static readonly Raiser OnDie = new();
        public static readonly Raiser OnJump = new();
        public static readonly Raiser OnFall = new();
        public static readonly Raiser OnLand = new();
        public static readonly Raiser OnSlide = new();

        AbilityManager _abilityManager;
        Character _character;
        void OnEnable() => _abilityManager.HookAbilities();
        void OnDisable() => _abilityManager.UnhookAbilities();

        void Awake()
        {
            _character = GetComponent<Character>();
            _abilityManager = new AbilityManager(GetComponents<Ability>());
            _abilityManager.InitAbilities();
        }

        void Update()
        {
            if (In.movePress)
            {
                if (In.turnHold)
                    OnTurn.Raise();
                else
                    OnSideStep.Raise();
            }

            if (In.slidePress)
                OnSlide.Raise();
                
            if(_character.state.isLanding)
                OnLand.Raise();
            
            if(In.jumpPress)
                OnJump.Raise();
            
            if(_character.state.isFalling)
                OnFall.Raise();
            
            //_character.UpdateCharacter();
            _abilityManager.UpdateAbilities();
            
            _character.Move(_character.vector3.vector);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayerMask(Block.layerMask))
                OnDie.Raise();
        }

        void OnDrawGizmos()
        {
            _abilityManager?.GizmosAbilities();
        }
    }
}