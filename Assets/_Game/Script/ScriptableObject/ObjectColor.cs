using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectColor : GameUnit
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer meshRenderer;
    public ColorType color;

    public void Awake()
    {
        OnInit();
        
    }

    public void OnInit()
    {
        ChangeColor(color);
    }

    public void ChangeColor(ColorType colorType)
    {
        color = colorType;
        meshRenderer.material = colorData.GetMat(colorType);
    }
}