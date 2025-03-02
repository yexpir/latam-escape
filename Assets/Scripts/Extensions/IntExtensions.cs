namespace Extensions
{
    public static class IntExtensions
    {
        public static int Mod(this int a, int mod)
        {
            a %= mod;
            return a;
        }
    }
}