using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdlBarrel1 : InteractionObjects
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals("Player") && _isActive)
		{
			UIManager.Instance.PopupUIActive(" 통 열기", true);
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals("Player") && _isActive)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				_isActive = false;
				UIManager.Instance.PopupUIActive("", false);
				StartCoroutine(PlayerHaveMoney(10, 5));
				ExpEffectSpawn(5);
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

	private void ExpEffectSpawn(int cycle)
	{
		for (int i = 0; i < cycle; i++)
		{
			GameObject expEffect = ObjectPoolManager.Instance.ObjectPoolPop("ExpEffect");
			expEffect.transform.position = transform.position;
			expEffect.SetActive(true);
		}
	}



	/// <summary>
	/// 사이클 수 만큼 돈을 지급
	/// </summary>
	/// <param name="money">지급할 돈</param>
	/// <param name="cycle">반복할 수</param>
	/// <returns></returns>
	private IEnumerator PlayerHaveMoney(int money, int cycle)
	{
		for (int i = 0; i < cycle; i++)
		{
			UIManager.Instance.IsHaveMoney(money);
			yield return new WaitForSeconds(0.5f);
		}
	}


}
