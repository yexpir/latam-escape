using System.Collections.Generic;
using UnityEngine;

namespace City.NEW.PoolStuff
{
    public class ObjectPooler : MonoBehaviour
    {
        public List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolsDictionary = new();
    }
}