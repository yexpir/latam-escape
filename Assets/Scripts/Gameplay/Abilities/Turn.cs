using System.Collections;
using System.Collections.Generic;
using CityStuff;
using Extensions;
using Gameplay.InputHandling;
using UnityEngine;
using Utils;
using Grid = Utils.Grid;

namespace Gameplay.Abilities
{
    public class Turn : Ability
    {
        [SerializeField] List<Transform> pointers = new();
        int _direction;
        float _streetStart;
        Vector3 _pivot;
        Vector3 _pivotOffset;

        float _turnSpeed;
        float _acceleration;
        public float Angle
        {
            get => _angle;
            set => _angle = M.Mod(value, 360f);
        } float _angle;

        void Start()
        {
            _turnSpeed = _character.data.turnSpeed;
            _acceleration = _turnSpeed * _character.data.acceleration;
        }

        void Update()
        {
            _turnSpeed += _acceleration;
        }


        public override void Init() => SetRaiser(PlayerController.OnTurn);

        protected override void Execute()
        {
            if(IsBlocked) return;
          
            if (_routine != null)
                StopCoroutine(_routine);
            
            _routine = StartCoroutine(Routine());
        }

        IEnumerator Routine()
        {
            _direction = In.xButton;
            _streetStart = Mathf.Round((City.map.totalLanesWidth / 2.0f + City.map.laneWidth) / City.map.cellSize) * City.map.cellSize;
            _pivotOffset = -_character.state.currentForward * _streetStart + _character.state.currentRight * (_direction * _streetStart);
            
            var intersection = _character.state.intersection;
            var pivot = intersection + _pivotOffset;
            var startingPosition = intersection - _character.state.currentForward * _streetStart;
            startingPosition = startingPosition.Project(_character.state.currentStreet.GetLane(_character.state.currentLaneIndex));
            
            var targetLaneIndex = _character.state.currentLaneIndex;
            if (_character.state.currentForward.Abs() == Vector3.forward && _direction == 1 || _character.state.currentForward.Abs() == Vector3.right && _direction == -1)
                targetLaneIndex = _character.state.currentStreet.lanes.Length - (targetLaneIndex + 1);
            
            var targetPosition = intersection + _character.state.currentRight * (_direction * _streetStart);
            targetPosition = targetPosition.Project(_character.state.nextStreet.GetLane(targetLaneIndex));
            
            //update intersection

            if (CityBuilder.IsInsideBlock(targetPosition + Vector3.up * 2))
                yield break;

            var targetForward = _character.state.currentRight * _direction;
            
            Angle = GetStartAngle(pivot, startingPosition);
            
            var startingRotation = Angle;
            var targetRotation = M.Mod(startingRotation + 90f * -_direction, 360f);
            
            pointers[0].position = pivot;
            pointers[1].position = startingPosition;
            pointers[2].position = targetPosition;
            
            while (!Grid.HasPassedPosition(transform, startingPosition))
            {
                if (IsBlocked)
                    yield break;
                yield return null;
            }

            transform.SetXZ(startingPosition);
            transform.rotation = Quaternion.Euler(transform.rotation.x, Angle, transform.rotation.z);
            var radius = Vector3.Distance(startingPosition, pivot);

            Activate();
            print("TURN");
            while (IsActive)
            {
                Angle += Time.deltaTime * (_turnSpeed / radius) * -_direction;
                var radians = Angle * Mathf.Deg2Rad;

                var circlePos = Vector3.zero;
                circlePos.x = Mathf.Cos(radians) * radius;
                circlePos.z = Mathf.Sin(radians) * radius;

                transform.SetXZ(pivot + circlePos);
                transform.rotation = Quaternion.Euler(0, -Angle, 0);
                transform.forward *= -_direction;

                if (!M.IsInRange(Angle, startingRotation, targetRotation))
                    break;
                
                yield return null;
            }
            transform.SetXZ(targetPosition);
            transform.forward = targetForward;
            _character.velocity = _character.velocity.SetXZ(Vector3.zero);
            _character.state.SetOrientation();
            _character.state.SetCurrentLaneIndex(targetLaneIndex);
            Deactivate();
            _routine = null;
        }
        static float GetStartAngle(Vector3 from, Vector3 to) => M.Mod(-Vector3.SignedAngle(Vector3.right, (to - from).Flatten().normalized, Vector3.up),360f);
    }
}