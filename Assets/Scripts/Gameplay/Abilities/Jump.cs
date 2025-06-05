using UnityEngine;

namespace Gameplay.Abilities
{
    public class Jump : Ability
    {
        public override void Init() => SetRaiser(PlayerController.OnJump);
        protected override void Execute()
        {
            _character.rigidBody.velocity = Vector3.up * _character.data.jumpForce;
        }
    }
}