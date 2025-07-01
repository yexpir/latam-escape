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
    }
}