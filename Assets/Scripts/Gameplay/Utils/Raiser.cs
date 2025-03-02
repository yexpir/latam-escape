using System;

namespace Gameplay.Utils
{
    public class Raiser
    {
        public event Action action;
        public void Raise() => action?.Invoke();
    }
}