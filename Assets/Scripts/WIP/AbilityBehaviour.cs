﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace WIP
{
    public abstract class AbilityBehaviour : ScriptableObject
    {
        protected Actor _actor;
        protected bool _isPaused;
        protected bool _isStopped;

        public void SetActor(Actor actor) => _actor = actor;
        
        public abstract IEnumerator Execute();
        public void Pause() => _isPaused = true;
        public void Resume() => _isPaused = false;
        public void Stop() => _isStopped = true;
        public void Reset() => _isStopped = false;
    }
}
