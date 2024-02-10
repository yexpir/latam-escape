using System.Collections.Generic;
using UnityEngine;
using Grid = WIP.Utils.Grid;

public class CoinSpawner : MonoBehaviour
{
    Vector3 PlayerPos => PlayerController.Instance.Position;

    public Coin _coinPrefab;
    public float spawnRadius;
    public int coinDensity;

    Zone _zone;
    int _unit;

    void Start()
    {
        _unit = (int)(CityBuilder.blockUnit + CityBuilder.streetWidth);
        var cityWidth = CityBuilder.cityWidth * _unit;
        var cityLength = CityBuilder.cityLength * _unit;
        _zone = new Zone(Vector3.zero, (int)cityWidth, (int)cityLength);
        //PopulateCity();
    }
    float timer;

    private void Update()
    {
        if(PlayerController.Instance == null) return;
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            for (var i = 0; i < coinDensity; i++)
            {
                var coinPos = GetRandomPositionInRadius(PlayerPos, spawnRadius * CityBuilder.cellSize);
                if(coinPos != null)
                {
                    if (IsInsideBuilding((Vector3)coinPos))
                    {
                        i--;
                        continue;
                    }
                    SpawnCoin((Vector3)coinPos);
                }
            }
        }
    }
    Coin SpawnCoin(Vector3 pos)
    {
        return Instantiate(_coinPrefab, pos, Quaternion.identity);
    }
    public static int CoinCount;
    public static void CountCoin()
    {
        CoinCount++;
    }
    static float RandomLane()
    {
        return (Random.Range(0, 3) - 1) * CityBuilder.cellSize;
    }

    HashSet<Vector3> coinPositions = new();
    Vector3? GetRandomPositionInRadius(Vector3 pos, float radius)
    {
        var newPos = (Vector3)Random.insideUnitCircle * radius;
        newPos.z = newPos.y;
        newPos.y = 0; 
        newPos = Grid.GetClosestStreetPosition(pos + newPos);
        if (coinPositions.Contains(newPos)) return null;
        coinPositions.Add(newPos);
        return newPos;
    }

    bool IsInsideBuilding(Vector3 pos)
    {
        return Physics.CheckSphere(pos, 0, Block.layerMask);
    }
}
