using System;
using CityStuff;
using CityStuff.PoolStuff.PrefabStuff;
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
        public static readonly Raiser OnSlide = new();
        
        public AbilityManager _abilityManager;
        Player _player;

        void Awake()
        {
            _abilityManager = new AbilityManager(GetComponents<Ability>());
            _abilityManager.InitAbilities();
            _player = GetComponent<Player>();
        }

        void OnEnable() => _abilityManager.HookAbilities();
        void OnDisable() => _abilityManager.UnhookAbilities();

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
                
            if(In.jumpPress)
                OnJump.Raise();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareLayerMask(Block.layerMask))
                OnDie.Raise();
        }
    }
}