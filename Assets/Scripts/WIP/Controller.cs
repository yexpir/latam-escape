using System.Linq;
using UnityEngine;

namespace WIP
{
    public class Controller : Actor
    {
        void Start(){
            ActionPool.FirstOrDefault(x => x.ID == 0).Play();
        }
    }
}