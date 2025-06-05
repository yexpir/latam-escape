using System;
using UnityEngine;

namespace Gameplay.Abilities
{
    [RequireComponent(typeof(ConstantForce))]
    public class Gravity : Ability
    {
        ConstantForce _cForce;
        [SerializeField] Vector3 _force;
        protected override void Awake()
        {
            base.Awake();
            _cForce = GetComponent<ConstantForce>();
        }

        void Start() => SetGravity(_force);

        public void SetGravity(Vector3 force) => _cForce.force = _force;
    }
}