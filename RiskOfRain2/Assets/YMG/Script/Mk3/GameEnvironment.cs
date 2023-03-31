using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public sealed class GameEnvironment : MonoBehaviour
{
    private static GameEnvironment instance;
	private List<GameObject> checkpoints = new List<GameObject>();
	public List<GameObject> Checkpoints { get { return checkpoints; } }

	public static GameEnvironment Singleton
	{
		get 
		{
			if (instance == null) 
			{
				instance = new GameEnvironment();
				//instance.Checkpoints.AddRange(
				//	GameObject.FindGameObjectWithTag("Checkpoint"));
			}
			return instance;
		}
	}
}
