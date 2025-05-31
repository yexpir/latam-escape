using System;
using System.Collections.Generic;
using System.Linq;
using CityStuff.PoolStuff;
using CityStuff.PoolStuff.PrefabStuff;
using Unity.VisualScripting;
using UnityEngine;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New World Object Set", menuName = "WorldObjectSet")]
    public class SO_WorldObjectSet : ScriptableObject
    {
        public List<PoolEntry> poolEntries = new();

        void OnValidate() => SetIds();

        void Awake() => SetIds();
        
        void SetIds()
        {
            foreach (var entry in poolEntries.Where(entry => entry?.prefab != null))
                entry.prefab.SetId(entry.id);
        }
    }
}