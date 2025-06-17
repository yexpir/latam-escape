using Gameplay.Abilities;
using UnityEngine;

namespace Gameplay
{
    public class Player : Character
    {
        Ability _turn;
        public bool IsTurning => _turn && _turn.IsActive;

        protected override void Awake()
        {
            base.Awake();
            _turn = GetComponent<Turn>();
        }
    }
}