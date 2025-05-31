using System;

namespace Utils
{
    public class Raiser
    {
        public event Action action;
        // ReSharper disable Unity.PerformanceAnalysis
        public void Raise() => action?.Invoke();
    }
}