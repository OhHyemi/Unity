using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MaterialColorChanger : MonoBehaviour
{
    public Color toColor;
    private Renderer _renderer;
    public Renderer Renderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
            }

            return _renderer;
        }
    }

    public void ChangeColor()
    {
        Renderer.material.color = toColor;
    }
    public void ChangeColorWithPropertyBlock()
    {
        var propertyBlock = new MaterialPropertyBlock();
        Renderer.GetPropertyBlock(propertyBlock);
        
        propertyBlock.SetColor("_Color", toColor);
        
        Renderer.SetPropertyBlock(propertyBlock);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ChangeColorWithPropertyBlock();
        }

        if (Input.GetKey(KeyCode.B))
        {
            ChangeColor();
        }
    }
}
