using System;
using CityStuff.PoolStuff;
using CityStuff.WorldDataStuff;
using UnityEngine;

namespace CityStuff.PrefabStuff.BaseObjectStuff
{
    [Serializable]
    public abstract class WorldObject : MonoBehaviour, IPoolable
    {
        public uint id { get; private set; }
        public ObjectData data;

        public virtual void Init(ObjectData newData, Transform parent)
        {
            data = newData;
            transform.position = data.position;
            transform.parent = parent;
        }
        
        public virtual void OnGet()
        {
            //Debug.Log($"{data.id} {name}");
            gameObject.SetActive(true);
        }

        public virtual void OnRelease()
        {
            gameObject.SetActive(false);
        }

        public void SetId(uint id)
        {
            this.id = id;
        }
    }
}