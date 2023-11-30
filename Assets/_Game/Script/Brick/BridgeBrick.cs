using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBrick : Brick
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagType.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            if(character.color != color && character.brickAmount > 0)
            {
                character.RemoveBrick();
                ChangeColor(character.color);
            }
        }
    }
}
