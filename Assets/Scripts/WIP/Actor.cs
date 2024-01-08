using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WIP
{
    public class Actor : MonoBehaviour
    {
        [field:SerializeField]public Character Character { get; }
        [field:SerializeField]private List<Ability> Actions = new();



        protected HashSet<AbilityHandler> ActionPool = new();
        protected HashSet<AbilityHandler> ActiveActions => ActionPool.Where(a => a.coroutine != null).ToHashSet();



        public void AddActiveAction(AbilityHandler action) => ActiveActions.Add(action);
        public void RemoveActiveAction(AbilityHandler action) => ActiveActions.Remove(action);



        void OnValidate()
        {
            ActionPool.Clear();
            foreach (var action in Actions)
            {
                print(this + "   " + action);
            }                
        }
    }
}