using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : GioleSingletone<UIManager>
{
	public UnityEvent interactionEvent = new UnityEvent();
	private PlayerUiManager _playerUiManagerCs = default;


	public new void Awake()
	{
		base.Awake();
		if (GioleFunc.GetRootObj("PlayerUiManager") == null) { /* Do nothing */ }
		else { _playerUiManagerCs = GioleFunc.GetRootObj("PlayerUiManager").GetComponent<PlayerUiManager>(); }
	}

	/// <summary>
	/// 플레이어의 상호작용 팝업 UI를 활성화 하는 함수
	/// </summary>
	/// <param name="text_">상호작용 팝업 UI에 작성할 텍스트</param>
	/// <param name="active_">True : 활성화, false : 비활성화</param>
	public void PopupUIActive(string text_, bool active_)
	{
		_playerUiManagerCs.InteractionPopupUIActive(text_, active_);
	}

	/// <summary>
	/// 플레이어의 돈을 관리하는 함수
	/// </summary>
	/// <param name="money_">+ 또는 - 의 값</param>
	/// <returns>성공시 True, 실패시 False</returns>
	public bool IsHaveMoney(int money_)
	{
		return _playerUiManagerCs.PlayerMoneyControl(money_);
	}

	public void PlayerHaveExp(int exp_)
	{
		_playerUiManagerCs.PlayerExpPlus(exp_);
	}


	/// <summary>
	/// 보스를 켜주고 이름을 설정하는 함수
	/// </summary>
	/// <param name="name_">이름</param>
	/// <param name="secondName_">두번째 이름</param>
	public void ActiveBoss(string name_, string secondName_)
	{
		_playerUiManagerCs.SetBossUi(name_, secondName_);
		_playerUiManagerCs.ActiveBossUi();
	}

	public void BossMissionComplete()
	{
		_playerUiManagerCs.CheckMissionUiComplate();
	}


	public void BossHpControl(int value_)
	{
		_playerUiManagerCs.BossHpControl(value_);
	}


	/// <summary>
	/// 페이드 아웃, 인 을 연출 할 수 있는 함수
	/// </summary>
	/// <param name="_fadeObj">화면을 가리는 페이드 이미지 오브젝트</param>
	/// <param name="loadSceneName">이동할 씬 이름, Nothing 입력시 씬 이동 X</param>
	/// <param name="selectFade">true : 페이드 아웃, false : 페이드 인</param>
	/// <returns></returns>
	public IEnumerator FadeWindow(GameObject _fadeObj, string loadSceneName, bool selectFade)
	{
		bool roop_ = true;
		float value_ = (selectFade) ? 0f : 1f;
		float changedValue_ = (selectFade) ? 0.02f : -0.02f;
		float maxValue = (selectFade) ? 1f : 0f;

		_fadeObj.SetImageColor(0f, 0f, 0f, value_);
		_fadeObj.SetActive(true);
		while (roop_)
		{
			yield return new WaitForSeconds(0.01f);
			value_ += changedValue_;
			_fadeObj.SetImageColor(0f, 0f, 0f, value_);
			switch (selectFade)
			{
				case true:
					if (maxValue < value_) { roop_ = false; }
					break;
				case false:
					if (maxValue > value_) { roop_ = false; }
					break;
			}
		}

		if (loadSceneName == "Nothing")
		{
			_fadeObj.SetActive(false);
			yield break;
		}
		GioleFunc.LoadScene(loadSceneName);
	}       // FadeWindow()

	/// <summary>
	/// 아이템을 획득하면 해당 아이템에서 스프라이트 랜더러의 이미지를 스코어 보드의 이미지에 추가하는 함수
	/// </summary>
	/// <param name="obj_"></param>
	public void AddItemList(GameObject obj_)
	{
		_playerUiManagerCs.AddItemList(obj_);
	}




}
