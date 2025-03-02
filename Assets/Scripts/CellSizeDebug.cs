using System.Collections;
using System.Collections.Generic;
using CityGeneration.Data;
using UnityEngine;

public class CellSizeDebug : MonoBehaviour
{
    [SerializeField] Transform[] corners;
    void Start()
    {
        corners[0].localPosition = ShapeData.vertices[0];
        corners[1].localPosition = ShapeData.vertices[1];
        corners[2].localPosition = ShapeData.vertices[2];
        corners[3].localPosition = ShapeData.vertices[3];
    }
}
