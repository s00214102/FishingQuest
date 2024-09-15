using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_State : BaseState
{
    PlayerStateMachine _stateMachine;

    public Explore_State(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine = stateMachine;
    }
    public override void Enter()
    {

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        _stateMachine.movement.DoUpdate();
        
        if (InputHandler.Instance.btnEastTriggered)
        {
            _stateMachine.SetState((int)PlayerState.Fishing);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        _stateMachine.movement.DoFixedUpdate();
    }
}
