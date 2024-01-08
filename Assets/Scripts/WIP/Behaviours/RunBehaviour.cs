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

        public override void SetActor(Actor actor)
        { 
            Debug.Log(actor + "RUUUNNN!!!");
            _transform = actor.gameObject.transform;
            _speed = actor.Character.travelSpeed;
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