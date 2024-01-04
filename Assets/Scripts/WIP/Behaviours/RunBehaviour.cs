using System;
using System.Collections;
using UnityEngine;

namespace WIP.Behaviours
{
    [Serializable]
    public class RunBehaviour : AbilityBehaviour
    {
        Transform _transform;
        float _speed;

        public RunBehaviour(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }
        
        public override IEnumerator Execute()
        {
            while (_transform.gameObject.activeSelf)
            {
                if (_isStopped) break;
                while (_isPaused) yield return null;
                _transform.position += _transform.forward * _speed; 
                yield return null;
            }
        }
    }
}