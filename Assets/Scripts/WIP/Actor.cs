using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WIP
{
    public class Actor : MonoBehaviour
    {
        public Character Character;
        [field:SerializeField]private List<Ability> Abilities = new();



        protected List<AbilityHandler> AbilityPool = new();
        protected List<AbilityHandler> ActiveAbilities => AbilityPool.Where(a => a.coroutine != null).ToList();



        public void AddActiveAction(AbilityHandler action) => ActiveAbilities.Add(action);
        public void RemoveActiveAction(AbilityHandler action) => ActiveAbilities.Remove(action);

        void OnValidate()
        {
            AbilityPool.Clear();
            foreach (var ability in Abilities)
            {
                if(ability != null)
                    AbilityPool.Add(new AbilityHandler(this, ability));
            }
        }
        void OnEnable(){
            AbilityPool.Clear();
            foreach (var ability in Abilities)
            {
                if(ability != null)
                    AbilityPool.Add(new AbilityHandler(this, ability));
            }
        }
    }
}