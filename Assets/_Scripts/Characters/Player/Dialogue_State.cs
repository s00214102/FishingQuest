using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_State : BaseState
{
    PlayerStateMachine _stateMachine;

    public Dialogue_State(PlayerStateMachine stateMachine) : base(stateMachine)
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
    }

}
