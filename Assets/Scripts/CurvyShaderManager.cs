using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvyShaderManager : MonoBehaviour
{
    public Shader defaultShader;

    public static CurvyShaderManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void Init(MonoBehaviour obj)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (var r in renderers)
        {
            var material = r.material;
    
            print(r.gameObject.name);
            if (material.shader.name == defaultShader.name)
            {
                material.shader = defaultShader;
            }
        }
    }
}
