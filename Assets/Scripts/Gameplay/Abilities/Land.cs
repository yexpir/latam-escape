using UnityEngine;

namespace Gameplay.Abilities
{
    public class Land : Ability
    {
        public override void Init() => SetRaiser(PlayerController.OnLand);
        public Transform groundhit;
        public Transform playerCenter;
        
        bool _isLandingAux;
        

        protected override void Execute()
        {
            if (IsBlocked) return;
            
            IsStopped = false;
            
            Activate();
        }

        public override void AbilityUpdate()
        {
            groundhit.position = _character.state.groundHit;
            playerCenter.position = _character.state.nextCenter;

            if(IsBlocked) return;
            
            _character.state.isLanding = !_isLandingAux && _character.state.isGrounded;
            _isLandingAux = _character.state.isGrounded;
            
            if (!_character.state.isGrounded) return;
            
            var groundYCorrection = _character.state.groundHit.y - transform.position.y;
            _character.vector3.Y = groundYCorrection;
            
            _character.state.fallSpeed = _character.data.fallSpeed;
            _character.state.fallCurve = _character.data.fallCurve;
        }
        
        public override void AbilityGizmos()
        {
            if(_character?.state == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_character.state.currentCenter, _character.state.groundHit);
        }
    }
}