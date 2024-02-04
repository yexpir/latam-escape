using Unity.VisualScripting;
using UnityEngine;

public class Grid
{	static float CellSize => CityBuilder.cellSize;
    static int Unit => (int)(CityBuilder.blockUnit + CityBuilder.streetWidth);

	public static Vector3 GetClosestPosition(Vector3 position)
	{
		position.x = Mathf.Round(position.x / CellSize) * CellSize;
		position.z = Mathf.Round(position.z / CellSize) * CellSize;
        return position;
	}

    static bool alternateStreet;
	public static Vector3 GetClosestStreetPosition(Vector3 pos)
	{
        if (alternateStreet)
        {
            pos.x = Mathf.Round(pos.x / Unit) * Unit + RandomLane();
            pos.z = Mathf.Round(pos.z / CellSize) * CellSize;
        }
        else
        {
            pos.x = Mathf.Round(pos.x / CellSize) * CellSize;
            pos.z = Mathf.Round(pos.z / Unit) * Unit + RandomLane();
        }
        alternateStreet = !alternateStreet;
		return pos;
	}

    static float RandomLane()
    {
        return (Random.Range(0, 3) - 1) * CityBuilder.cellSize;
    }

    /*
	 void PopulateCity()
    {
        var coinLength = (CityBuilder.cityLength + CityBuilder.streetWidth) * CityBuilder.blockUnit / CityBuilder.cellSize;
        var coinWidth = (CityBuilder.cityWidth + CityBuilder.streetWidth) * CityBuilder.blockUnit / CityBuilder.cellSize;
        for (int i = 0; i < CityBuilder.cityWidth; i++)
        {
            for (int j = 0; j < coinLength; j++)
            {
                Vector3 pos = Vector3.zero;
                pos.x = i * _unit + RandomLane();
                pos.z = j * CityBuilder.cellSize;
                SpawnCoin(pos);
            }
        }
        for (int i = 0; i < CityBuilder.cityLength; i++)
        {
            for (int j = 0; j < coinWidth; j++)
            {
                Vector3 pos = Vector3.zero;
                pos.x = j * CityBuilder.cellSize;
                pos.z = i * _unit + RandomLane();
                SpawnCoin(pos);
            }
        }
    }
	 */

    public static Vector3 GetNextPosition(Vector3 currentPosition, Vector3 forwardDirection)
    {
        Vector3 nextGridPosition = currentPosition + forwardDirection.normalized * CellSize;
        return GetClosestPosition(nextGridPosition);
    }

    public static bool HasPassedPosition(Transform movingTransform, Vector3 position)
    {
        return Vector3.Dot(position - movingTransform.position, movingTransform.forward) < 0;
    }
}