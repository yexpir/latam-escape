using CityStuff;
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
        
        public AbilityManager _abilityManager;

        void Awake()
        {
            _abilityManager = new AbilityManager(GetComponents<Ability>());
            _abilityManager.InitAbilities();
        }

        void OnEnable() => _abilityManager.HookAbilities();
        void OnDisable() => _abilityManager.UnhookAbilities();

        void Update()
        {
            if (CityBuilder.IsInsideBlock(transform.position))
                OnDie.Raise();

            if (In.movePress)
            {
                if (In.turnHold)
                    OnTurn.Raise();
                else
                    OnSideStep.Raise();
            }
            
            if(In.jumpPress)
                OnJump.Raise();
        }
    }
}