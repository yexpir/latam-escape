using System;
using System.Collections;
using UnityEngine;
using WIP.Utils;

namespace WIP.Behaviours
{
    [CreateAssetMenu(fileName = "New Behaviour", menuName = "Behaviours/RunBehaviour")]
    public class RunBehaviour : AbilityBehaviour
    {
        public Transform _transform;
        float _speed;

        Vector3 _nextGridPos;

        public override void Pause()
        {
            IsPausing = true;
            _nextGridPos = GridUtil.GetNextPosition(_transform.position, _transform.forward);
        }
        public override void Resume()
        {
            IsPaused = false;
            IsPausing = false;
        }
        public override void RequestEnd(){
            base.RequestEnd();
            Debug.Log("END?");
            _nextGridPos = GridUtil.GetNextPosition(_transform.position, _transform.forward);
        }

        public override IEnumerator Execute()
        {
            _transform = _actor.gameObject.transform;
            _speed = _actor.Character.travelSpeed;
            while (_transform.gameObject.activeSelf)
            {
                if (IsStopped)
                {
                    break;
                }
                while (IsPaused)
                {
                    yield return null;
                }

                var move = _transform.forward * (_speed * Time.deltaTime);
                _transform.position += move;

                if (IsPausing && GridUtil.HasPassedPosition(_transform, _nextGridPos))
                {
                    _transform.position = _nextGridPos;
                    IsPaused = true;
                }

                if(IsEnding && GridUtil.HasPassedPosition(_transform, _nextGridPos))
                {
                    _transform.position = _nextGridPos;
                    HasEnded = true;
                    yield break;
                }
                else yield return null;
            }
        }
    }
}