
using System;

namespace Gameplay.Abilities
{
    public class Death : Ability
    {
        public override void Init() => SetRaiser(PlayerController.OnDie);
        protected override void Execute() => foo();//GameManager.ResetCurrentScene();

        void foo()
        {
        }

        void OnEnable()
        {
            
        }
    }
}
