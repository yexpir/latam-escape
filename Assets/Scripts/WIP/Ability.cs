using Actions_Stuff;
using UnityEngine;

namespace WIP
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
    public class Ability : ScriptableObject
    {
        public int id;
        public string abilityName;
        public string description;
        public AbilityBehaviour behaviour;
    }
}