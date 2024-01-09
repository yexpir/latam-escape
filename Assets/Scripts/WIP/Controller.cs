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
    }
}