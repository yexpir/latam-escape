using Extensions;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Jump : Ability
    {
        public override void Init() => SetRaiser(PlayerController.OnJump);
        protected override void Execute()
        {
            _character.velocity.y = _character.data.jumpForce;
            print($"JUMP is grounded{_character.isGrounded}");
        }
    }
}