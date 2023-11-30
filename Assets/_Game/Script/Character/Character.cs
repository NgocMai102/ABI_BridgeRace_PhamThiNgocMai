using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Character;
using _Game.Framework.Event;

public class Character : ObjectColor
{
    [Header("---------------LayerMask---------------")] 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask slopeLayer;
    [SerializeField] private LayerMask gateLayer;

    [Space(10)] [Header("---------------Property---------------")] 
    [SerializeField] private Animator anim;
    [SerializeField] private Transform model;
    [SerializeField] protected Stage currentStage;

    [Space(10)] [Header("---------------Brick Collector---------------")] 
    [SerializeField] private Transform collectorTransform;

    [HideInInspector] public bool isFalling = false;
    private List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    private string currentAnimName;
    public int brickAmount => playerBricks.Count;
    public int CurrentStageId { get; private set; }

    private void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    protected virtual void OnInit()
    {
        animInit();
        model.rotation = Quaternion.identity;
        
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagType.CHARACTER))
        {
            Character character = other.GetComponent<Character>();
            if(character.brickAmount > brickAmount && brickAmount > 0)
            {
                DropBrick();
            }
        }
    }

    #region Animation

    protected void animInit()
    {
        currentAnimName = AnimType.IDLE;
    }

    protected void Idle()
    {
        ChangeAnim(AnimType.IDLE);
    }

    protected void Run()
    {
        ChangeAnim(AnimType.RUN);
    }

    protected void Fall()
    {
        ChangeAnim(AnimType.FALL);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            if (currentAnimName != null)
                anim.ResetTrigger(currentAnimName);
            anim.SetTrigger(currentAnimName);
        }
    }

    #endregion

    #region Movement
    protected Vector3 MoveTo(Vector3 target)
    {
        RaycastHit hit;
        if (Physics.Raycast(target, Vector3.down, out hit, 2f, groundLayer)) 
            return hit.point + Vector3.up * 0.4f;
        return TF.position;
    }

    public bool CheckBridge(Vector3 target)
    {
        // di len: cung mau gach -> di duoc
        //         khac mau gach -> con gach tren lung -> di duoc
        //                       -> khong co gach tren lung -> khong di duoc
        // di xuong: luon di duoc
        //TODO: giai quyet check mau gach 
        RaycastHit hit;
        if (Physics.Raycast(target, Vector3.down, out hit, 2f, slopeLayer))
        {
            BridgeBrick bridgeBrick = hit.collider.GetComponent<BridgeBrick>();
            if (bridgeBrick.color != color && brickAmount > 0)
            {
                ChangeColor(bridgeBrick.color);
                RemoveBrick();
            }
            if (bridgeBrick.color != color && brickAmount == 0 && model.forward.z > 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckGate()
    {
        RaycastHit hit;
        if (Physics.Raycast(TF.position, model.forward, out hit, 1f, gateLayer))
        {
            Gate gate = hit.collider.GetComponent<Gate>();

            if (gate.StageId != CurrentStageId)
            {
                 OnNextStage(gate);
                 this.PostEvent(EventID.CharacterOnNextStage, this);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveTo(Vector3 target)
    {
        return CheckBridge(target) && CheckGate();
    }
    
    protected void RotateFollow(Vector3 target)
    {
        Vector3 vectorTarget = target - TF.position;
        vectorTarget.y = 0;
        model.forward = vectorTarget;
    }
    #endregion

    #region Collect Brick
    public virtual void AddBrick()
    {
        PlayerBrick brick = SimplePool.Spawn<PlayerBrick>(
            PoolType.PlayerBrick, 
            Vector3.up * 0.22f * playerBricks.Count,
            Quaternion.identity, 
            collectorTransform
        );
        brick.ChangeColor(color);
        playerBricks.Add(brick);
    }
    
    public virtual void RemoveBrick() {
        SimplePool.Despawn(playerBricks[playerBricks.Count - 1]);
        playerBricks.RemoveAt(playerBricks.Count - 1);
    }

    public virtual void DropBrick()
    {
        while (brickAmount > 0)
        {
            RemoveBrick();
        }
    }
    #endregion
    
    public virtual void OnWin()
    {
        ChangeAnim(AnimType.WIN);
    }
    
    public IEnumerator ChangePosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.position = targetPosition;
    }
    
    protected virtual void OnNextStage(Gate gate)
    {
        CurrentStageId = gate.StageId;
        StartCoroutine(ChangePosition(TF.position + Vector3.forward * 2f, 0.2f));
    }
    
    public void SetStage(Stage stage)
    {
        currentStage = stage;
    }
}