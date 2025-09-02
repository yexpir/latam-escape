using Extensions;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Run : Ability
    {
        public float moveSpeed;
        float _acceleration;

        void Start()
        {
            moveSpeed = _character.data.forwardSpeed;
            _acceleration = moveSpeed * _character.data.acceleration;
        }

        public override void AbilityUpdate()
        {
            moveSpeed += _acceleration;
            var movement = _character.state.currentForward * (moveSpeed * Time.deltaTime);
            _character.velocity.vector = _character.velocity.vector.Project(movement);
        }

        public override void Init() => SetRaiser(PlayerController.OnRun);
    }
}