using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slope : MonoBehaviour
{
    [SerializeField] private int stepCount;
    [SerializeField] private List<BridgeBrick> listBridgeBricks = new List<BridgeBrick>();
    [SerializeField] private Vector3 brickOffset;
    
    [Space(20)]
    [SerializeField] private Transform slope;
    [SerializeField] private Transform rope;

    private Vector3 spawnPos;
    private static readonly Vector3 BridgeBrickSize = BridgeBrickUtils.SIZE;

    private void Awake()
    {
        brickOffset.y = BridgeBrickSize.y;
    }

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        SpawnBridge();
        
        SetSlope();
        
        SetRope();
    }

    private void SpawnBridge()
    {
        for(int i = 0; i < stepCount; i++)
        {
            spawnPos.Set(0, brickOffset.y * i, brickOffset.z * i + BridgeBrickSize.z / 2);
            BridgeBrick brick = SimplePool.Spawn<BridgeBrick>(PoolType.BridgeBrick, spawnPos, Quaternion.identity, transform);
            listBridgeBricks.Add(brick);
        }
    }
    
    //TODO: Try to optimize these functions
    private void SetSlope()
    {
        slope.localScale = new Vector3(0.2f, 1, 0.08f * stepCount);
        
        float slopeAngle = -Mathf.Atan(BridgeBrickSize.y / brickOffset.z);
        slope.localRotation = Quaternion.Euler(slopeAngle * Mathf.Rad2Deg, 0, 0);
        
        float slopeLength = Mathf.Sqrt(BridgeBrickSize.y * BridgeBrickSize.y + brickOffset.z * brickOffset.z) * stepCount - brickOffset.z * brickOffset.z;
        slope.localPosition = new Vector3(0, -slopeLength / 2 * Mathf.Sin(slopeAngle), slopeLength / 2 * Mathf.Cos(slopeAngle));
    }

    private void SetRope()
    {
        rope.localScale = new Vector3(1, 1, 0.08f * stepCount);
        
        float slopeAngle = -Mathf.Atan(BridgeBrickSize.y / brickOffset.z);
        rope.localRotation = Quaternion.Euler(slopeAngle * Mathf.Rad2Deg, 0, 0);
        
        float slopeLength = Mathf.Sqrt(BridgeBrickSize.y * BridgeBrickSize.y + brickOffset.z * brickOffset.z) * stepCount - brickOffset.z * brickOffset.z;
        rope.localPosition = new Vector3(0, -slopeLength / 2 * Mathf.Sin(slopeAngle), slopeLength / 2 * Mathf.Cos(slopeAngle));
    }
}
