using System.Collections.Generic;
using Actions_Stuff;
using UnityEngine;

namespace WIP
{
    public class Actor : MonoBehaviour
    {
        List<ActionRoutine> _activeActions = new();

        HashSet<ActionRoutine> _pool = new();
        public HashSet<ActionRoutine> ActionPool => _pool;
        
        public void AddAction(ActionRoutine actionRoutine)
        {
            _pool.Add(actionRoutine);
        }

        public void AddActiveAction(ActionRoutine action)
        {
            _activeActions.Add(action);
        }
    }
}