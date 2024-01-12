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

        public void SetDirection(int direction) => _direction = direction;
        
        public override IEnumerator Execute()
        {
            _transform = _actor.transform;
            _speed = _actor.Character.turnSpeed;

            var radius = CityBuilder.sidestepSize;
            var direction = _direction;
            var time = 0.0f;

            var rotationPoint = _transform.position + _transform.right * (radius * _direction);

            while(true){
                if (_isStopped) break;
                while (_isPaused) yield return null;

                time += Time.deltaTime * _speed * _direction;
                var nextPos = rotationPoint;
                nextPos.x += Mathf.Sin(time) * radius;
                nextPos.z += Mathf.Cos(time) * radius;
                nextPos.y += 0.0f;
                _transform.position = nextPos;
                
                yield return null;
            }
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