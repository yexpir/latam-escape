using System;
using System.Collections.Generic;
using Actions_Stuff;

namespace DefaultNamespace
{
    public class ActionPool
    {
        static HashSet<ActionRoutine> pool = new();
        public static HashSet<ActionRoutine> GetPool => pool;
        public static void AddToPool(ActionRoutine actionRoutine) => pool.Add(actionRoutine);
    }
}