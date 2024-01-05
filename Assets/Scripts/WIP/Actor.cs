using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WIP
{
    public class Actor : MonoBehaviour
    {
        [field:SerializeField]public Character Character { get; }

        public HashSet<AbilityHandler> ActiveActions => ActionPool.Where(a => a.coroutine != null).ToHashSet();

        public List<Ability> Actions = new();
        List<AbilityHandler> ActionPool = new();
        [SerializeField]public AbilityHandler actionA;
        public void AddActiveAction(AbilityHandler action) => ActiveActions.Add(action);
        public void RemoveActiveAction(AbilityHandler action) => ActiveActions.Remove(action);

        void OnValidate()
        {
            foreach (var action in Actions)
                ActionPool.Add(new AbilityHandler(this, action));
        }
    }
}