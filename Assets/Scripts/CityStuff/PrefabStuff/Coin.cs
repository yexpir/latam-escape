using CityStuff.PrefabStuff.BaseObjectStuff;

namespace CityStuff.PrefabStuff
{
    public class Coin : Pickup
    {
        void OnEnable() => OnPickup.action += OnCoinPickup;
        void OnDisable() => OnPickup.action -= OnCoinPickup;

        void OnCoinPickup()
        {
            print("COIN PICKUP");
        }
    }
}