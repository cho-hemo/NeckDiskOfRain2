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
		public GameObject playerPrefab = default;
		#endregion
		private PlayerBase _player = default;
		private PlayerController _playerController = default;
		private List<Skill> _skills = default;

		public int coin = default;

		#region Property
		public Transform PlayerTransform { get { return _playerTransform; } private set { _playerTransform = value; } }
		public PlayerBase Player { get { return _player; } private set { _player = value; } }
		public PlayerController PlayerController { get { return _playerController; } private set { _playerController = value; } }
		public List<Skill> Skills { get { return _skills; } private set { _skills = value; } }
		#endregion

		// { 게임의 난이도를 선택하기 위한 변수
		public Difficulty GameDiffi { get; set; }
		// { 게임의 난이도를 선택하기 위한 변수


		private new void Awake()
		{
			base.Awake();
			Global.AddOnSceneLoaded(OnSceneLoaded);
		}
		private void Start()
		{

		}

		/// <summary>
		/// Scene이 로드 되었을 때 호출되는 함수
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="mode"></param>
		public void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
		{
			switch (scene.name)
			{
				case Global.INIT_SCENE_NAME:
					Global.LoadScene(Global.TITLE_SCENE_NAME);
					break;
				case Global.PLAY_SCENE_NAME:
					PlayerCreate();
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 플레이어 생성
		/// </summary>
		private void PlayerCreate()
		{
			Instantiate(playerPrefab, new Vector3(-60f, -140f, -120f), Quaternion.identity).name = playerPrefab.name;
			PlayerInit();
		}

		/// <summary>
		/// 플레이어 초기화
		/// </summary>
		private void PlayerInit()
		{
			PlayerTransform = Global.FindRootObject("Player").transform;
			PlayerTransform.TryGetComponent<PlayerBase>(out _player);
			PlayerTransform.TryGetComponent<PlayerController>(out _playerController);
			KeyInputManager.Instance.SetPlayerController(_playerController);
			Skills = Player.Skills;
		}

		/// <summary>
		/// 총알 타격시 호출되는 함수
		/// </summary>
		public void BulletOnCollision(GameObject obj, int skillIndex)
		{
			float damage = _player.AttackDamage * _player.Skills[skillIndex].Multiplier;
			Debug.Log($"데미지를 입힘 damage : {damage}");
			Debug.Log($"Obj : {obj.name} / tag : {obj.tag}");
			if (obj.tag == "Boss")
			{
				obj.GetComponent<BossMonsterBase>().OnDamaged((int)damage);
			}
			else
			{
				obj.GetComponent<MonsterBase>().OnDamaged((int)damage);
			}
		}

		public void AddCoin(int value)
		{
			coin += value;
		}

		/// <summary>
		/// 경험치 볼을 생성하는 함수
		/// </summary>
		/// <param name="cycle">경험치볼의 생성 개수</param>
		/// <param name="tf_">생성할 곳의 트랜스폼</param>
		public void ExpEffectSpawn(int cycle, Transform tf_)
		{
			for (int i = 0; i < cycle; i++)
			{
				GameObject expEffect = ObjectPoolManager.Instance.ObjectPoolPop("ExpEffect");
				expEffect.transform.position = tf_.position;
				expEffect.SetActive(true);
			}
		}

	}
}
