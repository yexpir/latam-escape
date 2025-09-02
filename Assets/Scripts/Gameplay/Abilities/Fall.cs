using System.Collections;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Fall : Ability
    {
        float _a, _c, _h;
        bool _isFallingAux;
        
        public override void Init() => SetRaiser(PlayerController.OnFall);

        protected override void Execute()
        {
            print("FALL");
            if (IsBlocked) return;
            if (_character.state.isGrounded) return;
            
            Activate();

            if (_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());
        }

        public override void AbilityUpdate()
        {
            _character.state.isFalling = !_isFallingAux && !_character.state.isGrounded && !IsBlocked;
            _isFallingAux = !_character.state.isGrounded && !IsBlocked;
        }

        IEnumerator Routine()
        {
            _a = _character.state.fallSpeed;
            _c = transform.position.y;
            _h = _character.state.fallCurve;
            var startTime = Time.time;
            while (IsActive)
            {
                var x = Time.time - startTime;
                var y = F(x);
                if (y < _character.state.groundHit.y) break;
                var yVel = y - _character.transform.position.y;
                _character.velocity.Y = yVel;
                yield return null;
            }
            Deactivate();
            _character.velocity.Y = 0;
            var p = _character.transform.position;
            p.y = _character.state.groundHit.y;
            _character.transform.position = p;
        }
        
        float F(float x)
        {
            return -_a * x * (x + _h) + _c;
        }
    }
}