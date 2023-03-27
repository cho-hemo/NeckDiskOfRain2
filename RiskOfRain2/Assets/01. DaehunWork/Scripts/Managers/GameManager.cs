using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiskOfRain2.Manager
{
	public class GameManager : SingletonBase<GameManager>
	{
		#region Inspector
		[SerializeField]
		[Tooltip("플레이어의 Transform")]
		private Transform _playerTransform = default;
		#endregion

		#region Property
		public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }
		#endregion

		private new void Awake()
		{
			base.Awake();
			Global.AddOnSceneLoaded(OnSceneLoaded);
			PlayerTransform = Global.FindRootObject("Player").transform;
		}
		private void Start()
		{

		}

		public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			switch (scene.name)
			{
				case "":
					break;
				default:
					break;
			}
		}
	}
}