using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : InteractionObjects
{
	private bool _isActive = true;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && _isActive)
		{
			UIManager.Instance.PopupUIActive(" 텔레포터 가동..?", true);
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (other.tag.Equals("Player") && _isActive)
			{
				_isActive = false;
				UIManager.Instance.PopupUIActive("", false);
				GioleFunc.GetRootObj("mdlBeetleQueen (6)").SetActive(true);
				UIManager.Instance.ActiveBoss("여왕 딱정벌레", "무리의 어미");
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals("Player"))
		{
			UIManager.Instance.PopupUIActive("", false);
		}
	}





}
