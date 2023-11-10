using System;
using Unity.VisualScripting;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int probability;
    Material _material;

    public static LayerMask layerMask = 1<<8;

    public Transform cube;
    
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
}