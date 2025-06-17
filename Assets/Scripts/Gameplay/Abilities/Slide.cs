using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Abilities
{
    public class Slide : Ability
    {
        [SerializeField]Mesh _standingMesh, _slidingMesh;
        

        public override void Init() => SetRaiser(PlayerController.OnSlide);

        protected override void Execute()
        {
            if(IsBlocked) return;

            if (_routine != null)
                StopCoroutine(_routine);
            
            _routine = StartCoroutine(Routine());
        }

        IEnumerator Routine()
        {
            IsActive = true;
            Begin();
            yield return new WaitForSeconds(0.75f);
            End();
            yield return new WaitForSeconds(0.25f);
            IsActive = false;
        }


        void Begin()
        {
            _character.hitbox.height = 1;
            _character.hitbox.center = new Vector3(0.0f, 0.5f, 0.0f);
            _character.meshFilter.mesh = _slidingMesh;
            _character.meshFilter.transform.localPosition = new Vector3(0.0f, 0.5f, 0.0f);
        }

        void End()
        {
            _character.hitbox.height = 2;
            _character.hitbox.center = new Vector3(0.0f, 1.0f, 0.0f);
            _character.meshFilter.mesh = _standingMesh;
            _character.meshFilter.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        }
    }
}