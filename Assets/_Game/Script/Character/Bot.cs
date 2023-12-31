using System.Collections;
using System.Collections.Generic;
using _Game.Utils;
using UnityEngine;
using _Game.Character;
using _Game.Framework.StateMachine;
using _Game.Pattern.StateMachine;
using _Game.Framework.Event;

public class Bot : Character
{
    [Header("Components")]
    [SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent;
    
    [Header("Config")] 
    [SerializeField] public BotConfig botConfig;
    
    [Header("Properties")]
    [SerializeField] private Transform finishPoint;
    
    private bool _isGoingToFinishPoint = false;
    private IState<Bot> _currentState;
    private List<Vector3> _listBrickPos = new List<Vector3>();
    public BotConfig BotConfig => botConfig;
    public Vector3 NextPosition => navMeshAgent.nextPosition;

    #region States
    public IdleState IdleState { get; private set; }
    public CollectState CollectState { get; private set; }
    public MoveToFinishPointState MoveToFinishPointState { get; private set; }
    public FallState FallState { get; private set; }

    #endregion

    private void Awake()
    {
        IdleState = new IdleState();
        CollectState = new CollectState();
        MoveToFinishPointState = new MoveToFinishPointState();
        FallState = new FallState();
    }
    protected override void OnInit()
    {
        base.OnInit();
        
        navMeshAgent.speed = botConfig.moveSpeed;
        ChangeState(IdleState);
    }
    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState<Bot> state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }
    private void MoveToPosition(Vector3 position)
    {
        navMeshAgent.enabled = true;
        navMeshAgent.SetDestination(position);
        ChangeAnim(AnimType.RUN);
    }
    private void AllowMoveToFinishPoint()
    {
        if (_isGoingToFinishPoint)
        {
            return;
        }
        
        float random = Random.Range(0f, 1f);
        if (random >= 0.9f)
        {
            _isGoingToFinishPoint = true;
            ChangeState(MoveToFinishPointState);
        }
        else
        {
            MoveToRandomBrick();
        }
    }
    public void MoveToFinishPos()
    {
        MoveToPosition(finishPoint.position);   
    }
    public void MoveToRandomBrick()
    {
        _listBrickPos = currentStage.GetListPosBrickTakeable(color);
        
        if (_listBrickPos.Count > 0)
        {
            int randomIndex = Random.Range(0, _listBrickPos.Count);
            MoveToPosition(_listBrickPos[randomIndex]);
        }
        else
        {
            ChangeState(MoveToFinishPointState);
        }
    }   
    public override void AddBrick()
    {
        base.AddBrick();
        AllowMoveToFinishPoint();
    }
    public override void DropBrick()
    {
        base.DropBrick();
        ChangeState(FallState);
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }
    public void NotEnoughBrick()
    {
        _isGoingToFinishPoint = false;
        ChangeState(CollectState);
    }

    public override void OnWin()
    {
        base.OnWin();
        this.PostEvent(EventID.PlayerLose);
    }

    
}