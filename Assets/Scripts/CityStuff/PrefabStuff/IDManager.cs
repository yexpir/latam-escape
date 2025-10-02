using System;
using System.Linq;
using Utils;

namespace CityStuff.PrefabStuff
{
    public static class IDManager
    {
        public static int chunkContainerID { get; private set; }
        public static int blockID { get; private set; }
        public static int barrierID { get; private set; }
        static void InitID(string name)
        {
            var wobj = City.worldObjectSet.GetWorldObjectByName(name);
            switch (name)
            {
                case "chunkContainer":
                    chunkContainerID = wobj.id;
                    break;
                case "block":
                    blockID = wobj.id;
                    break;
                case "barrier":
                    barrierID = wobj.id;
                    break;
            }
        }

        public static void Init()
        {
            foreach (var wobj in City.worldObjectSet.poolEntries.Select(e => e.prefab))
                InitID(wobj.name);
        }
    }
}