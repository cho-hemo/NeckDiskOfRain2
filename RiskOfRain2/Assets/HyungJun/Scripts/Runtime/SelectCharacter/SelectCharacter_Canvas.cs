using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiskOfRain2;

public class SelectCharacter_Canvas : MonoBehaviour
{
	private GameObject _fadeWindow = default;
	private void Start()
	{
		_fadeWindow = gameObject.FindChildObj("FadeWindow");
		StartCoroutine(UIManager.Instance.FadeWindow(_fadeWindow, "Nothing", false));
	}

	// private IEnumerator FadeInWindow()
	// {
	// 	float value_ = 1f;
	// 	while (true)
	// 	{
	// 		yield return new WaitForSeconds(0.01f);
	// 		value_ -= 0.02f;
	// 		_fadeWindow.SetImageColor(0f, 0f, 0f, value_);
	// 		if (value_ < 0f) { break; }
	// 	}
	// 	_fadeWindow.SetActive(false);
	// }

	public void ReadyBtnClick()
	{
		StartCoroutine(UIManager.Instance.FadeWindow(_fadeWindow, Global.PLAY_SCENE_NAME, true));
	}

	public void BackBtnClick()
	{
		StartCoroutine(UIManager.Instance.FadeWindow(_fadeWindow, Global.TITLE_SCENE_NAME, true));
	}
}
