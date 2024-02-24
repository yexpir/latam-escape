using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideObjectsInFrontOfPlayer : MonoBehaviour
{
    Renderer[] _renderers;
    int _opacityHash;

    void Awake()
    {
        _opacityHash = Shader.PropertyToID("_Opacity");
    }

    void OnTriggerEnter(Collider other)
    {
        _renderers = other.transform.parent.GetComponentsInChildren<Renderer>();
        print("Hide");
        foreach (var r in _renderers)
            r.material.SetFloat(_opacityHash, 0.25f);
    }

    void OnTriggerExit(Collider other)
    {
        foreach (var r in _renderers)
            r.material.SetFloat(_opacityHash, 1);
    }
}
