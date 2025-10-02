using System.Collections.Generic;
using System.Linq;
using CityStuff.PoolStuff;
using CityStuff.PrefabStuff.BaseObjectStuff;
using CityStuff.PrefabStuff;
using EditorAttributes;
using Extensions;
using UnityEditor;
using UnityEngine;

namespace CityStuff.ConfigurationStuff
{
    [CreateAssetMenu(fileName = "New World Object Set", menuName = "WorldObjectSet")]
    public class SO_WorldObjectSet : ScriptableObject
    {
        public List<PoolEntry> poolEntries = new();


        [Button("ApplyPrefabs")]
        void ApplyChangesToPrefabs()
        {
            var prefabs = poolEntries.Select(a => a.prefab).ToList();
            for (var i = 0; i < prefabs.Count; i++)
            {
                poolEntries[i].id = i;
                var path = AssetDatabase.GetAssetPath(prefabs[i]);
                var prefabRoot = PrefabUtility.LoadPrefabContents(path);
                var wobj = prefabRoot.GetComponent<WorldObject>();
                if (wobj)
                    wobj.SetId(i);

                PrefabUtility.SaveAsPrefabAsset(prefabRoot, path);
                PrefabUtility.UnloadPrefabContents(prefabRoot);
            }
        }
        public WorldObject GetWorldObjectByName(string wobjName) => poolEntries.Select(e => e.prefab).FirstOrDefault(p => p.name == wobjName);
    }
}