using System.Collections;
using System.Collections.Generic;
using CMF;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[System.Serializable]
public enum PlayerState
{
    Idle, // player has no control, used for stuff like cutscenes
    Explore, // moving around the world
    Dialogue, // talking to an NPC
    Fishing, // fishing
}

public class PlayerStateMachine : BaseStateMachine
{
    public PlayerState CurrentState;

    Dictionary<PlayerState, BaseState> States = new();

    //public AdvancedWalkerController walkerController;
    public SimplePlayerMovement movement;
    private void Awake()
    {
        //walkerController = GetComponent<AdvancedWalkerController>();
        movement = GetComponent<SimplePlayerMovement>();
    }

    void Start()
    {
        // cache components

        InitiliazeStates();

        SetState(1);
    }
    private void Update()
    {
        CurrentImplimentation.Update();
    }

    private void FixedUpdate()
    {
        CurrentImplimentation.FixedUpdate();
    }
    private void InitiliazeStates()
    {
        States.Add(PlayerState.Idle, new Idle_State(this));
        States.Add(PlayerState.Explore, new Explore_State(this));
        States.Add(PlayerState.Dialogue, new Dialogue_State(this));
        States.Add(PlayerState.Fishing, new Fishing_State(this));
    }

    public override void SetState(int newState)
    {
        CurrentState = (PlayerState)newState;

        if (States.ContainsKey(CurrentState))
        {
            // if another state is running, exit before switching to new state
            if (CurrentImplimentation != null)
                CurrentImplimentation.Exit();

            CurrentImplimentation = States[CurrentState];
            CurrentImplimentation.Enter();
        }

        base.SetState(newState);
    }
}
