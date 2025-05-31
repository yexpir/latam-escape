using System.Collections;
using Extensions;
using Gameplay.InputHandling;
using UnityEngine;
using Utils;
using Grid = Utils.Grid;

namespace Gameplay.Abilities
{
    public class Sidestep : Ability
    {
        Vector3 _movement;
        bool _isMovingInZ;
        float _target;
        float _prevTarget;
        Vector3 _LRAxis;

        public Transform _pointer1;
        
        public override void Init() => SetRaiser(PlayerController.OnSideStep);

        protected override void Execute()
        {
            if(IsBlocked)
                return;

            var targetLaneIndex = GetNexLaneIndex(In.xButton);
            if (_character.state.currentLaneIndex == targetLaneIndex) return;
            
            IsActive = true;
            _target = _character.state.currentStreet.lanes[targetLaneIndex];
            _character.state.SetCurrentLaneIndex(targetLaneIndex);
            
            _isMovingInZ = Mathf.Abs(transform.forward.z) > Mathf.Abs(transform.forward.x);
            
            _movement = transform.right * (In.xButton * _character.data.sideStepSpeed * Time.deltaTime);

            //_target = Street.GetClosestStreetLaneInDirection(transform.position, transform.right * In.xButton);//Street.GetNextLaneInDirection(transform, transform.right * In.xButton).Mult(transform.right.Round().Abs()).Max();
            var target = transform.position.ProjectWithSelector(transform.right.Abs() * _target, transform.right);
            _pointer1.position = target;
        }

        void Update()
        {
            if(!IsActive) return;
            
            var pos = _isMovingInZ ? transform.position.x : transform.position.z;
            var nextPos = pos + _movement.Max();
            
            var currDistance = Mathf.Abs(_target - pos);
            var nextDistance = Mathf.Abs(_target - nextPos);
            
            if (nextDistance >= currDistance) IsActive = false;
            
            if (IsActive) _character.Move(_movement);
            else transform.SetXorZ(_isMovingInZ, _target);
        }

        IEnumerator Routine()
        {
            IsActive = true;
            
            //sidestep speed
            var isMovingInZ = Mathf.Abs(transform.forward.z) > Mathf.Abs(transform.forward.x);
            var startPos = transform.position;
            var targetPos = Grid.GetNextPosition(startPos, transform.right * In.xInt);
            var prevDistance = Grid.CellSize;
            var dir = In.xInt ;
            while (IsActive)
            {
                var movement = transform.right * (dir * _character.data.sideStepSpeed * Time.deltaTime);
                
                float target;
                float pos;
                
                if (isMovingInZ)
                {
                    target = targetPos.x;
                    pos = transform.position.x;
                    transform.MoveX(movement.x);
                }
                else
                {
                    target = targetPos.z;
                    pos = transform.position.z;
                    transform.MoveZ(movement.z);
                } 
                
                var currDistance = Mathf.Abs(target - pos);
                if (currDistance > prevDistance)
                    break;
                prevDistance = currDistance;
                
                yield return null;
            }
            
            if(isMovingInZ)
                transform.SetX(targetPos.x);
            else
                transform.SetZ(targetPos.z);
            
            IsActive = false;
        }

        int GetNexLaneIndex(int direction)
        {
            var nextIndex = _character.state.currentLaneIndex + Mathf.FloorToInt(transform.right.Round().Max()) * direction;
            return Mathf.Clamp(nextIndex, 0, City.map.laneCount - 1); 
        }
    }
}
