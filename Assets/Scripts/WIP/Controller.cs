using System.Linq;
using UnityEngine;

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
                AbilityPool[0].Pause();
                AbilityPool[2].Stop();
                AbilityPool[1].Play();
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                AbilityPool[0].Pause();
                AbilityPool[1].Stop();
                AbilityPool[2].Play();
            }
            if(Input.GetKeyUp(KeyCode.RightArrow)){
                AbilityPool[2].Stop();
            }
            if(Input.GetKeyUp(KeyCode.LeftArrow)){
                AbilityPool[1].Stop();
            }
            if(!AbilityPool[1].IsActive && !AbilityPool[2].IsActive){
                AbilityPool[0].Resume();
            }
        }
    }
}