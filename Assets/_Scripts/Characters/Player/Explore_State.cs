using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore_State : BaseState
{
    PlayerController _controller;

    public Explore_State(PlayerController controller) : base(controller)
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
        base.Update();
        
        _controller.walkerController.DoUpdate();
        
        if (InputHandler.Instance.btnEastTriggered)
        {
            _controller.SetState((int)PlayerState.Fishing);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        
        _controller.walkerController.DoFixedUpdate();
    }
}
