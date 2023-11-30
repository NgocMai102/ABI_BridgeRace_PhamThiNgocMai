using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ColorData", menuName = "Scriptable/ColorData")]
public class ColorData : ScriptableObject
{
    public Material[] materials;
    public Material GetMat(ColorType colorType)
    {
        return materials[(int)colorType];
    } 
}

