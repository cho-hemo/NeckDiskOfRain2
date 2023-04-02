using RiskOfRain2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2.Manager;

public class Teleporter : InteractionObjects
{
	[SerializeField] private List<GameObject> _bossMonsters;
	private Transform _bossSpawnSpot;
	private bool _gameover = false;

	private void Start()
	{
		_bossSpawnSpot = Global.FindRootObject("BossSpawnSpot").transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && _isActive)
		{
			_disposable = true;
			UIManager.Instance.PopupUIActive(" 텔레포터 가동..?", true);
		}
		else if (other.tag.Equals("Player") && GameManager.Instance.IsBossDie)
		{
			_gameover = true;
			UIManager.Instance.PopupUIActive(" 텔레포터 가동..?", true);
		}
	}

	private void Update()
	{
		Debug.Log(GameManager.Instance.IsBossDie);
		if (Input.GetKeyDown(KeyCode.E) && _disposable)
		{
			Interaction();

		}
		else if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.IsBossDie && _gameover)
		{
			KeyInputManager.Instance.SetCursorState(false);
			GioleFunc.LoadScene("04. EndingScene");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			_disposable = false;
			UIManager.Instance.PopupUIActive("", false);
		}
	}

	public override void Interaction()
	{
		base.Interaction();
		_isActive = false;
		_disposable = false;
		// 목표 UI오브젝트 세팅을 바꿔주는 함수
		UIManager.Instance.MissionComplete();
		UIManager.Instance.PopupUIActive("", false);

		int randomNum = Random.Range(0, _bossMonsters.Count);
		GameObject boss = Instantiate(_bossMonsters[randomNum], _bossSpawnSpot.position, _bossSpawnSpot.rotation);
	}



}
