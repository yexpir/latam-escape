using System.Collections;
using UnityEngine;

namespace States
{
    public abstract class BaseState
    {
        Coroutine routine;
        public abstract IEnumerator Enter(TravelerOld travelerOld);
        public abstract IEnumerator Execute(TravelerOld travelerOld);
        public abstract IEnumerator Exit(TravelerOld travelerOld);
    }
}