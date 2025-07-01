using System.Collections;
using Extensions;
using TMPro;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Jump : Ability
    {
        float _a;
        Vector3 _startPosition;
        float _yOffset;
        public override void Init() => SetRaiser(PlayerController.OnJump);
        protected override void Execute()
        {
            if (IsBlocked) return;
            IsActive = true;

            if (_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());
        }

        IEnumerator Routine()
        {
            print("JUMP");
            

            _startPosition = transform.position.Flatten();
            _yOffset = transform.position.y;
            var startTime = Time.time;
            while (IsActive)
            {
                var x = Time.time - startTime;
                var y = f(x) + _yOffset;
                if (y < 0) break;
                var yVel = y - _character.transform.position.y;
                _character.velocity.y = yVel;
                yield return null;
            }
            IsActive = false;
            _character.velocity.y = 0;
            var p = _character.transform.position;
            p.y = 0.0f;
            _character.transform.position = p;
        }

        float f(float x)
        {
            _a = 4 * _c / (_h * _h);
            return _a * x * (_h - x);
        }
        float _h => _character.data.jumpDuration;
        float _c => _character.data.jumpHeight;
    }
}