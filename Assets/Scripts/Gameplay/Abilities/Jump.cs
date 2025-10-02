using System.Collections;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Jump : Ability
    {
        float _startingHeight;
        public override void Init() => SetRaiser(PlayerController.OnJump);
        protected override void Execute()
        {
            if (IsBlocked) return;
            if (!_character.state.isGrounded) return;
            
            Activate();

            if (_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(Routine());
        }
        
        //-----------------JUMP INPUT BUFFER---------------------
        // var t = _character.data.jumpBuffer;
        //     while (!_character.state.isGrounded)
        // {
        //     t -= Time.deltaTime;
        //     if(t < 0f)
        //         yield break;
        //     yield return null;
        // }


        IEnumerator Routine()
        {
            _startingHeight = _character.state.groundHit.y;
            var startTime = Time.time;
            while (IsActive)
            {
                var x = Time.time - startTime;
                var y = F(x);
                var yVel = y - _character.transform.position.y;
                if (yVel < 0f)
                    break;
                _character.vector3.Y = yVel;
                yield return null;
            }

            if (!IsActive) yield break;
            
            Deactivate();
            
            _character.vector3.Y = 0;
        }

        float F(float x)
        {
            var h = _character.data.jumpDuration;
            var c = _character.data.jumpHeight;
            var a = 4 * c / (h * h);
            return a * x * (h - x) + _startingHeight;
        }
    }
}