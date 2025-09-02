using System.Collections;
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
            _character.state.fallSpeed = _character.data.fastFallSpeed;
            _character.state.fallCurve = _character.data.fastFallCurve;
            Activate();
        }
        void End()
        {
            _character.hitbox.height = 2;
            _character.hitbox.center = new Vector3(0.0f, 1.0f, 0.0f);
            _character.meshFilter.mesh = _standingMesh;
            _character.meshFilter.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
            Deactivate();
        }

        public override void Stop()
        {
            base.Stop();
            End();
        }
    }
}