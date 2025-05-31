using System;
using CityStuff.WorldDataStuff;
using UnityEngine;

namespace CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff
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