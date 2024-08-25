using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public PlayerController playerController;

	void Start()
	{
		InitPlayer();
	}

	private void InitPlayer()
	{
		if (playerController != null)
		{
			playerController.SetState((int)PlayerState.Explore);
		}
		else
		{
			Debug.LogError("PlayerController reference is missing in GameManager.");
		}
	}
}
