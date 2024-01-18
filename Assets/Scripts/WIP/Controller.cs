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
            //activeAbilities = AbilityPool.Where(a => a.coroutine != null).ToList();
            var line1 = "";
            var line2 = "";
            foreach (var ability in AbilityPool)
            {
                line1 += "-----" + ability.AbilityName + "-----";
                line2 += "-----" + ability.state + "-----";
            }

            //print("\n" + line1 + "\n" + line2);
        }
    }
}