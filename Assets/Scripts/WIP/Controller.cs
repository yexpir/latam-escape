using System.Linq;
using UnityEngine;
using System.Collections.Generic;



namespace WIP
{
    public class Controller : Actor
    {
        public List<AbilityHandler> activeAbilities = new();
        
        void Start()
        {
            AbilityPool[0].Play();
        }

        void Update(){
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                AbilityPool[1].Play();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                AbilityPool[2].Play();
            }
            activeAbilities = AbilityPool.Where(a => a.state == AbilityState.Active).ToList();
            var line1 = "";
            foreach (var ability in activeAbilities)
            {
                line1 += "-----" + ability.AbilityName + "-----";
            }

            //print(line1);
        }
    }
}