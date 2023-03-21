using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : SingletonBase<UIManager>
{
	public UnityEvent interactionEvent = new UnityEvent();
	private PlayerUiManager _playerUiManagerCs = default;


	public new void Awake()
	{
		base.Awake();
		_playerUiManagerCs = GioleFunc.GetRootObj("PlayerUiManager").GetComponent<PlayerUiManager>();
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


	public void BossHpControl(int value_)
	{
		_playerUiManagerCs.BossHpControl(value_);
	}




}
