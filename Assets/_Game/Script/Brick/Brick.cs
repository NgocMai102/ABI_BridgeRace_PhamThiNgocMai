using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ObjectColor
{
    protected void Start()
    {
        
    }

    protected virtual void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
