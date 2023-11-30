using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBrick : Brick
{
    private bool isTakeAble = false;

    private void OnEnable()
    {
        isTakeAble = false;
        StartCoroutine(TakeAble());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isTakeAble)
        {
            return;
        }
        if (other.CompareTag(TagType.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            if (character.color == color)
            {
                StopCoroutine(TakeAble());
                character.AddBrick();
                OnDespawn();
            }
        }
    }
    
    private IEnumerator TakeAble()
    {
        yield return new WaitForSeconds(1f);
        isTakeAble = true;
    }
}
