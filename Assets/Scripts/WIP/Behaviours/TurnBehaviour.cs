using System;
using System.Collections;
using UnityEngine;

namespace WIP.Behaviours
{
    [CreateAssetMenu(fileName = "New Behaviour", menuName = "Behaviours/TurnBehaviour")]
    public class TurnBehaviour : AbilityBehaviour
    {
        Transform _transform;
        float _speed;
        [SerializeField] int _direction;
        public const float circumference = Mathf.PI * 2;
        public const float circumferenceStep = circumference/4;

        public void SetDirection(int direction) => _direction = direction;

        int facingDirection{
            get{
                var dir = _transform.forward;
                float x = Mathf.Abs(dir.x);
                float z = Mathf.Abs(dir.z);

                if (_direction == -1)
                {
                    if (x > z)
                    {
                        if (dir.x > 0)
                            return 2; // Flipped Left
                        else
                            return 0; // Flipped Right
                    }
                    else
                    {
                        if (dir.z > 0)
                            return 1; // Flipped Down
                        else
                            return 3; // Flipped Up
                    }
                }
                else
                {
                    if (x > z)
                    {
                        if (dir.x > 0)
                            return 0; // Right
                        else
                            return 2; // Left
                    }
                    else
                    {
                        if (dir.z > 0)
                            return 3; // Up
                        else
                            return 1; // Down
                    }
                }
            }
        }

        public override IEnumerator Execute()
        {
            if (IsStopped) yield break;
            _transform = _actor.transform;
            _speed = _actor.Character.turnSpeed;

            var radius = CityBuilder.sidestepSize;
            var direction = _direction;
            var time = 0.0f;

            var rotationPoint = _transform.position + _transform.right * (radius * _direction);

            var targetPosition = _transform.position + _transform.forward * radius + _transform.right * (radius * _direction);
            var targetFacingDirection = _transform.right * _direction;
            var facingDir = facingDirection;

            while(true){
                while (IsPaused) yield return null;

                time += Time.deltaTime * _speed * _direction;

                if(Mathf.Abs(time) >= circumference) time = 0.0f;
                if(Mathf.Abs(time) >= circumferenceStep){
                    _transform.position = targetPosition;
                    _transform.forward = targetFacingDirection;
                    break;
                }

                var nextPos = rotationPoint;
                var radian = time+circumferenceStep * facingDir;

                nextPos.x += Mathf.Sin(radian) * radius;
                nextPos.z += Mathf.Cos(radian) * radius;
                nextPos.y += 0.0f;

                _transform.position = nextPos;
                _transform.right = (rotationPoint - _transform.position) * _direction;
                
                yield return null;
            }
            HasEnded = true;
        }
    }
}





            /* var rotationPoint = _transform.position + _transform.right * (CityBuilder.sidestepSize * _direction);
            var targetPosition = _transform.position;
            targetPosition += _transform.right * (CityBuilder.sidestepSize * _direction);
            targetPosition += _transform.forward * (CityBuilder.sidestepSize);
            var targetForwardDirection = _transform.right * _direction;
            float totalRotationAngle = 0.0f;
            while (true)
            {
                if (_isStopped) break;
                while (_isPaused) yield return null;
                
                var linearDistance = _speed * Time.deltaTime;

                var relativePosition = _transform.position - rotationPoint;

                var rotationAngle = linearDistance / (2 * Mathf.PI * relativePosition.magnitude) * 360 * -_direction;

                totalRotationAngle += Mathf.Abs(rotationAngle);
                Debug.Log(rotationAngle);
                if(totalRotationAngle >= 90.0f)
                {
                    _transform.position = targetPosition;
                    _transform.forward = targetForwardDirection;
                    break;
                }

                var newX = Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.x - Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;
                var newZ = Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.x + Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;

                var nextPosition = new Vector3(newX, relativePosition.y, newZ) + rotationPoint;
                var delta = nextPosition - _transform.position;
                delta.y = 0;
                _transform.position += delta;
                _transform.right = (rotationPoint - _transform.position) * _direction;
                yield return null;
            } */