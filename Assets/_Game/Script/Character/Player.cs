using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using _Game.Framework.Event;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player: Character
{
    [Header("---------------Player Property---------------")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed;
    private Vector3 moveVector3;

    private void Awake()
    {
        base.Awake();
        OnInit();
    }
    void Start()
    {
        
    }

    void OnInit()
    {
       base.OnInit();
       if (joystick == null)
       {
           joystick = FindObjectOfType<FloatingJoystick>();
       }

       CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
       if (cameraFollow != null)
       {
           cameraFollow.SetTarget(TF);
       }
    }
    
    void Update()
    {
        PlayerMovement();
    }

    #region Movement

    private void PlayerMovement()
    {
        if (isFalling)
        {
            return;
        }
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            RotateFollow(TargetMove());
            if (CanMoveTo(TargetMove()))
            {
                TF.position = MoveTo(TargetMove());
            }
            Run();
            return;
        }
        Idle();
    }

    private Vector3 TargetMove()
    {
        moveVector3 = Vector3.zero;
        moveVector3.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector3.z = joystick.Vertical * moveSpeed * Time.deltaTime;
        return moveVector3 + TF.position;
    }

    #endregion

    public override void OnWin()
    {
        base.OnWin();
        this.PostEvent(EventID.PlayerWin);
    }
    private void FixedUpdate()
    {
        
        
    }
}
