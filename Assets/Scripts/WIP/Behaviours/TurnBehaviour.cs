using System;
using System.Collections;
using UnityEngine;

namespace WIP.Behaviours
{
    [Serializable]
    public class TurnBehaviour : AbilityBehaviour
    {
        Transform _transform;
        float _speed;
        int _direction;

        public TurnBehaviour(Transform transform, float speed, int direction)
        {
            _transform = transform;
            _speed = speed;
            _direction = direction;
        }

        public void SetDirection(int direction) => _direction = direction;
        
        public override IEnumerator Execute()
        {
            var rotationPoint = _transform.position + _transform.right * (CityBuilder.stepsideSize * _direction);
            while (true)
            {
                if (_isStopped) break;
                while (_isPaused) yield return null;
                
                var linearDistance = _speed * Time.deltaTime;

                var relativePosition = _transform.position - rotationPoint;

                var rotationAngle = linearDistance / (2 * Mathf.PI * relativePosition.magnitude) * 360 * -_direction;

                var newX = Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.x - Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;
                var newZ = Mathf.Sin(Mathf.Deg2Rad * rotationAngle) * relativePosition.x + Mathf.Cos(Mathf.Deg2Rad * rotationAngle) * relativePosition.z;

                var nextPosition = new Vector3(newX, relativePosition.y, newZ) + rotationPoint;
                var delta = nextPosition - _transform.position;
                delta.y = 0;
                _transform.position += delta;
                _transform.right = (rotationPoint - _transform.position) * _direction;
                yield return null;
            }
        }
    }
}