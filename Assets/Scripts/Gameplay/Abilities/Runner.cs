using System;
using Extensions;
using UnityEngine;
using Utils;

namespace Gameplay.Abilities
{
    public class Runner : Ability
    {
        public float _moveSpeed;
        float _acceleration;

        void Start()
        {
            _moveSpeed = _character.data.forwardSpeed;
            _acceleration = _moveSpeed * _character.data.acceleration;
        }

        void Update()
        {
            _moveSpeed += _acceleration;
            var movement = _character.state.currentForward * (_moveSpeed * Time.deltaTime);
            _character.velocity = _character.velocity.Project(movement);
        }

        public override void Init() => SetRaiser(PlayerController.OnRun);
    }
}