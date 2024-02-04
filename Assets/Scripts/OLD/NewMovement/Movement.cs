using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    Vector3 axis;
    // Start is called before the first frame update
    void Start()
    {
        axis = transform.position + transform.right * CityBuilder.cellSize;
    }

    // Update is called once per frame
    void Update()
    {
        Move(CalculateDisplacement(axis, CityBuilder.cellSize, 5.0f, -1));
    }

    void Move(Vector3 movement)
    {
        transform.position += movement;
    }

    Vector3 CalculateDisplacement(Vector3 pivot, float radius, float speed, int direction)
    {
        float angleRad = speed * Mathf.Deg2Rad * direction; // Convert speed to radians
        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);

        // Adjust the displacement based on the pivot's position
        Vector3 pivotToPosition = transform.position - pivot;
        Vector3 rotatedPosition = new Vector3(pivotToPosition.x * cosAngle - pivotToPosition.z * sinAngle,
                                              pivotToPosition.y,
                                              pivotToPosition.x * sinAngle + pivotToPosition.z * cosAngle);
        rotatedPosition.x = pivotToPosition.x * cosAngle - pivotToPosition.z * sinAngle;
        rotatedPosition.z = pivotToPosition.x * sinAngle + pivotToPosition.z * cosAngle;
        rotatedPosition.y = 0.0f;

        transform.right = (axis - transform.position) * -direction;

        // Calculate the final displacement vector
        return rotatedPosition - pivotToPosition;
    }
}
