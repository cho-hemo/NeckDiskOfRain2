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
		UIManager.Instance.PopupUIActive("", false);
		GioleFunc.GetRootObj("mdlBeetleQueen (6)").SetActive(true);
		UIManager.Instance.ActiveBoss("여왕 딱정벌레", "무리의 어미");

	}



}
