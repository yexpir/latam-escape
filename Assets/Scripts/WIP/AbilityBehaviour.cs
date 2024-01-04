using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace WIP
{
    [Serializable]
    public class AbilityBehaviour : ScriptableObject
    {
        public int OMG;

        protected bool _isPaused;
        protected bool _isStopped;

        public virtual IEnumerator Execute()
        {
            return null;
        }
        //
        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;
        public void Stop() => _isStopped = true;
    }
}
