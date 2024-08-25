using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : BaseState
{
    PlayerController _controller;

    public Idle_State(PlayerController controller) : base(controller)
    {
        _controller = controller;
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

    }

}
