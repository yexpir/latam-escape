namespace CityStuff.PoolStuff
{
    public interface IPoolable
    {
        public void OnGet();
        public void OnRelease();
    }
}