using System.Collections;
using System.Collections.Generic;

public abstract class BaseState
{
    public BaseState(BaseStateMachine stateMachine) { }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
