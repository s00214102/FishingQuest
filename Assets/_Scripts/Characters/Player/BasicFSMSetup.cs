using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FSM_Template : BaseStateMachine
{
	public enum EntityState
	{
		Explore // moves through the dungeon
	}
	public EntityState CurrentState;
	Dictionary<EntityState, BaseState> States = new Dictionary<EntityState, BaseState>();


	void Start()
	{
		InitiliazeStates();

		SetState(0);
	}
	private void Update()
	{
		CurrentImplimentation.Update();
	}
	private void InitiliazeStates()
	{
		// States.Add(HeroState.Explore, new Hero_Explore_State(this));
	}
	public override void SetState(int newState)
	{
		CurrentState = (EntityState)newState;

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
