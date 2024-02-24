using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    public int probability;
    List<Material> _materials = new();

    public static LayerMask layerMask = 1<<8;

    public Transform cube;

    float blockUnit;
    float longUnit;
    float pivotOffset;

    int _colorHash;

    void Awake()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
            _materials.Add(r.material);
        
        _colorHash = Shader.PropertyToID("_BaseColor");
    }

    public void SetColor(Color color)
    {
        foreach (var m in _materials)
            m.SetColor(_colorHash, color);
    }

    public void SetRandomSolor()
    {
        foreach (var m in _materials)
            m.SetColor(_colorHash, Random.ColorHSV());
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
        cube.localPosition = Vector3.zero;
    }
    public void Big()
    {
        cube.localScale = new Vector3(longUnit, blockUnit, longUnit);
        cube.localPosition = new Vector3(pivotOffset, 0, pivotOffset);
    }
    public void Horizontal()
    {

    }
    public void Vertical()
    {

    }
}