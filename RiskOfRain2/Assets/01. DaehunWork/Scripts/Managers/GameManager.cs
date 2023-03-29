using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Player;

namespace RiskOfRain2.Manager
{
	public class GameManager : SingletonBase<GameManager>
	{
		#region Inspector
		[SerializeField]
		[Tooltip("플레이어의 Transform")]
		private Transform _playerTransform = default;
		private PlayerBase _player = default;
		private List<Skill> _skills = default;
		#endregion

		#region Property
		public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }
		public PlayerBase Player { get { return _player; } private set { _player = value; } }
		public List<Skill> Skills { get { return _skills; } private set { _skills = value; } }
		#endregion

		// { 게임의 난이도를 선택하기 위한 변수
		public Difficulty GameDiffi { get; set; }
		// { 게임의 난이도를 선택하기 위한 변수


		private new void Awake()
		{
			base.Awake();
			Global.AddOnSceneLoaded(OnSceneLoaded);
			PlayerTransform = Global.FindRootObject("Player").transform;
			Player = PlayerTransform.GetComponent<PlayerBase>();
		}
		private void Start()
		{
			Skills = Player.Skills;
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
