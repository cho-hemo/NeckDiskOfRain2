using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractionObjects
{
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
		UIManager.Instance.BossMissionComplete();
		UIManager.Instance.PopupUIActive("", false);
		GameObject bossObj_ = GioleFunc.GetRootObj("BeetleQueen");
		bossObj_.SetActive(true);
		bossObj_.GetComponent<RootMotion>().Init();
		UIManager.Instance.ActiveBoss("여왕 딱정벌레", "무리의 어미");

	}



}
