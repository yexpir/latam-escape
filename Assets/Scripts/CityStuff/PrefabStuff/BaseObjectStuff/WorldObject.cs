using System;
using CityStuff.PoolStuff;
using CityStuff.WorldDataStuff;
using UnityEngine;
using Utils;

namespace CityStuff.PrefabStuff.BaseObjectStuff
{
    [Serializable]
    public abstract class WorldObject : MonoBehaviour, IPoolable
    {
        [field:SerializeField]public int id { get; private set; }
        public ObjectData data;

        public virtual void Init(ObjectData newData)
        {
            data = newData;
            transform.position = data.position;
            transform.rotation = data.rotation;
            transform.SetParent(data.chunkContainer.transform);
        }
        
        public virtual void OnGet()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnRelease()
        {
            gameObject.SetActive(false);
        }

        public void SetId(int newID)
        {
            id = newID;
        }
    }
}