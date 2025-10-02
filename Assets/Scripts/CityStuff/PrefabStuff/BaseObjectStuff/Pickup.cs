using Extensions;
using UnityEngine;
using Utils;

namespace CityStuff.PrefabStuff.BaseObjectStuff
{
    public class Pickup : WorldObject
    {
        [SerializeField] LayerMask layerMask;
        public readonly Raiser OnPickup = new();

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareLayer(layerMask.value))
                OnPickup.Raise();
        }
    }
}