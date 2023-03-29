using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Title_Canvas : MonoBehaviour
{
	private GameObject _fadeObj = default;
	private void Start()
	{
		_fadeObj = gameObject.FindChildObj("FadeWindow");
		StartCoroutine(UIManager.Instance.FadeWindow(_fadeObj, "Nothing", false));
	}

	public void StartGame_SingleBtnClick()
	{
		StartCoroutine(UIManager.Instance.FadeWindow(_fadeObj, GioleData.SELECT_CHARACTER_SCENE_NAME, true));
	}

	public void ExitGameBtnClick()
	{
		GioleFunc.QuitThisGame();
	}


	// // 화면 페이드 아웃
	// private IEnumerator FadeOut(string loadSceneName)
	// {
	// 	float value_ = 0f;
	// 	_fadeOutObj.SetImageColor(0f, 0f, 0f, value_);
	// 	_fadeOutObj.SetActive(true);
	// 	while (true)
	// 	{
	// 		yield return new WaitForSeconds(0.01f);
	// 		value_ += 0.02f;
	// 		_fadeOutObj.SetImageColor(0f, 0f, 0f, value_);
	// 		if (1f < value_) { break; }
	// 	}
	// 	GioleFunc.LoadScene(loadSceneName);
	// }

}
