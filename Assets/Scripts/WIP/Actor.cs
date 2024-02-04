using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WIP
{
    public class Actor : MonoBehaviour
    {
        public Character Character;
        
        [field:SerializeField]protected List<AbilityHandler> AbilityPool = new(); 
        public List<AbilityHandler> ActiveAbilities => AbilityPool.Where(a => a.state == AbilityState.Active).ToList();

        public void AddActiveAction(AbilityHandler action) => ActiveAbilities.Add(action);
        public void RemoveActiveAction(AbilityHandler action) => ActiveAbilities.Remove(action);

        void OnEnable(){
            foreach(AbilityHandler a in AbilityPool){
                if(a.Ability == null) continue;
                a.Init(this);
            }
        }
    }
}