using System;
using System.Collections;
using UnityEngine;

namespace WIP.Behaviours
{
    [CreateAssetMenu(fileName = "New Behaviour", menuName = "Behaviours/RunBehaviour")]
    public class RunBehaviour : AbilityBehaviour
    {
        Transform _transform;
        float _speed;

        public override IEnumerator Execute()
        {
            _transform = _actor.gameObject.transform;
            _speed = _actor.Character.travelSpeed;
            while (_transform.gameObject.activeSelf)
            {
                if (_isStopped) break;
                while (_isPaused) yield return null;
                _transform.position += _transform.forward * (_speed * Time.deltaTime); 
                yield return null;
            }
        }
    }
}