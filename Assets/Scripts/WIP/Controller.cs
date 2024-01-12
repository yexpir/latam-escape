using System.Linq;
using UnityEngine;
using System.Collections;


namespace WIP
{
    public class Controller : Actor
    {
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
            if(Input.GetKeyUp(KeyCode.LeftArrow)){
                AbilityPool[1].Stop();
            }
            if(Input.GetKeyUp(KeyCode.RightArrow)){
                AbilityPool[2].Stop();
            }
        }
    }
}