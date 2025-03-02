using System;
using Extensions;
using UnityEngine;

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
            transform.Move(transform.forward * (_moveSpeed * Time.deltaTime));
        }

        public override void Init() => SetRaiser(PlayerController.OnRun);
    }
}