using System.Collections;
using Extensions;
using TMPro;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Jump : Ability
    {
        float _a, _h, _c;
        public override void Init() => SetRaiser(PlayerController.OnJump);
        protected override void Execute()
        {
            if (IsBlocked) return;
            IsActive = true;

            _h = _character.data.jumpDistance;
            _c = _character.data.jumpHeight;
            _a = 4 * _c / (_h * _h);
            
            if (_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());

            // _character.velocity.y = _character.data.jumpHeight;
            // _character.isGrounded = false;
        }

        IEnumerator Routine()
        {
            var xOffset = transform.position.Flatten().Select(_character.state.currentForward).Abs();
            while (IsActive)
            {
                var x = transform.position.Flatten().Select(_character.state.currentForward).Abs() - xOffset;
                var y = f(x);
                if (y < 0) break;
                var yVel = y - _character.transform.position.y;
                _character.velocity.y = yVel;
                yield return null;
            }
            IsActive = false;
            _character.velocity.y = 0;
            var p = _character.transform.position;
            _character.transform.position = new Vector3(p.x, 0, p.z);
        }

        float f(float x)
        {
            return _a * x * (_h - x);
        }
    }
}