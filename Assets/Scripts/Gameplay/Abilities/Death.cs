
using System;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Death : Ability
    {
        public override void Init() => SetRaiser(PlayerController.OnDie);
        protected override void Execute() => Die();

        [SerializeField] public bool _enableDeath;

        void Die()
        {
            if(_enableDeath)
                GameManager.ResetCurrentScene();
        }
    }
}
