using RiskOfRain2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractionObjects
{
	[SerializeField] private List<GameObject> _bossMonsters;
	private Transform _bossSpawnSpot;

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
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && _disposable)
		{
			Interaction();
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
