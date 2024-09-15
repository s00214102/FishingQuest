using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameManager();
			}

			return _instance;
		}
	}

	public enum GameState
	{
		Tutorial,
		Play,
		Pause,
	}

	public GameState CurrentState { get; private set; }

	public PlayerStateMachine playerStateMachine;

	void Start()
	{
		InitPlayer();
	}

	private void InitPlayer()
	{
		if (playerStateMachine != null)
		{
			playerStateMachine.SetState((int)PlayerState.Explore);
		}
		else
		{
			Debug.LogError("PlayerStateMachine reference is missing in GameManager.");
		}
	}
}
