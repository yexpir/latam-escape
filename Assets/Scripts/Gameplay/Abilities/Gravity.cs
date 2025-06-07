using System;
using Extensions;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Gravity : Ability
    {
        Vector3 _velocity;
        void Update()
        {
            if (!_character.isGrounded)
            {
                if(_character.velocity.y > _character.data.fallingThreshold) 
                    _velocity.y = -_character.data.jumpGravity * Time.deltaTime;
                else
                    _velocity.y = -_character.data.fallGravity * Time.deltaTime;
            }
            else
                _velocity.y = 0f;
            _character.velocity.y += _velocity.y * Time.deltaTime;
            _character.velocity.y = Mathf.Clamp(_character.velocity.y, -_character.data.maxFallSpeed, float.MaxValue);
        }
    }
}