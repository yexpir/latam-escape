using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace WIP
{
    public abstract class AbilityBehaviour : ScriptableObject
    {
        protected bool _isPaused;
        protected bool _isStopped;

        public abstract IEnumerator Execute();
        
        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;
        public void Stop() => _isStopped = true;
    }
}
