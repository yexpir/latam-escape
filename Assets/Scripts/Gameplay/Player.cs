using Gameplay.Abilities;
using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        public bool IsTurning => _turn && _turn.IsActive;
        Ability _turn;

        protected override void Awake()
        {
            base.Awake();
            _turn = GetComponent<Turn>();
        }
    }
}