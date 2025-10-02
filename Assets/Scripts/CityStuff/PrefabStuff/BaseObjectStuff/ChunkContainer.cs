using CityStuff.PoolStuff;

namespace CityStuff.PrefabStuff.BaseObjectStuff
{
    public class ChunkContainer : WorldObject
    {
        public WorldObject[] segmentContainers;
        public override void OnGet()
        {
            segmentContainers = GetComponentsInChildren<WorldObject>();
            base.OnGet();
        }
    }
}