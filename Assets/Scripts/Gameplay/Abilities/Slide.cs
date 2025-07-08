using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Slide : Ability
    {
        [SerializeField]Mesh _standingMesh, _slidingMesh;
        Coroutine _fastfallRoutine;
        float _a, _c;


        public override void Init() => SetRaiser(PlayerController.OnSlide);

        protected override void Execute()
        {
            if(IsBlocked) return;

            IsStopped = false;
            
            if (_routine != null)
                StopCoroutine(_routine);
            
            _routine = StartCoroutine(Routine());
        }

        IEnumerator Routine()
        {
            Begin();
            yield return new WaitForSeconds(0.75f);
            End();
            yield return new WaitForSeconds(0.25f);
            Deactivate();
        }


        void Begin()
        {
            _character.hitbox.height = 1;
            _character.hitbox.center = new Vector3(0.0f, 0.5f, 0.0f);
            _character.meshFilter.mesh = _slidingMesh;
            _character.meshFilter.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
            _character.state.fallingSpeed = _character.data.fastFallSpeed;

            if (_fastfallRoutine != null)
                StopCoroutine(_fastfallRoutine);
            _fastfallRoutine = StartCoroutine(FastfallRoutine());
        }
        IEnumerator FastfallRoutine()
        {
            while (_character.state.isGrounded)
            {
                if (IsStopped)
                    yield break;
                yield return null;
            }
            
            print("SLIDE");
            Activate();

            _a = _character.data.fastFallSpeed;
            _c = transform.position.y;
            var startTime = Time.time;
            while (IsActive)
            {
                var x = Time.time - startTime;
                var y = f(x);
                if (y < 0) break;
                var yVel = y - _character.transform.position.y;
                _character.velocity.y = yVel;
                yield return null;
            }
            Deactivate();;
            _character.velocity.y = 0;
            var p = _character.transform.position;
            p.y = 0.0f;
            _character.transform.position = p;
        }

        void End()
        {
            _character.hitbox.height = 2;
            _character.hitbox.center = new Vector3(0.0f, 1.0f, 0.0f);
            _character.meshFilter.mesh = _standingMesh;
            _character.meshFilter.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        }

        float f(float x)
        {
            //f(x)=−ax(x+1)+c
            return -_a * x * (x + 1) + _c;
        }
    }
}