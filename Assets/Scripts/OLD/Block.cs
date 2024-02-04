using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Block : MonoBehaviour
{
    public int probability;
    Material _material;

    public static LayerMask layerMask = 1<<8;

    public Transform cube;

    float blockUnit;
    float longUnit;
    float pivotOffset;

    void Awake()
    {
        _material = GetComponentInChildren<Renderer>().material;
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    public void SetScale(float height)
    {
        transform.localScale = new Vector3(transform.localScale.x, height , transform.localScale.z);
    }

    public void SetBlockUnit(float blockUnit) => this.blockUnit = blockUnit;
    public void SetLongUnit(float longUnit) => this.longUnit = longUnit;
    public void SetPivotOffset(float pivotOffset) => this.pivotOffset = pivotOffset;
    public void Init(float blockUnit, float longUnit, float pivotOffset)
    {
        SetBlockUnit(blockUnit);
        SetLongUnit(longUnit);
        SetPivotOffset(pivotOffset);
    }

    public void Regular()
    {
        cube.localScale = new Vector3(blockUnit, blockUnit, blockUnit);
        cube.localPosition = new Vector3(0, blockUnit / 2, 0);
    }
    public void Big()
    {
        cube.localScale = new Vector3(longUnit, blockUnit, longUnit);
        cube.localPosition = new Vector3(pivotOffset, blockUnit / 2, pivotOffset);
    }
    public void Horizontal()
    {

    }
    public void Vertical()
    {

    }
}