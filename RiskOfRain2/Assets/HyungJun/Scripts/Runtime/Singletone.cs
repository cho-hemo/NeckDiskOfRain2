using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone : GioleSingletone<Singletone>
{
    private PlayerUiManager _uiManagerCs = default;

    public override void Awake()
    {
        base.Awake();
        _uiManagerCs = GioleFunc.GetRootObj("PlayerUiManager").GetComponent<PlayerUiManager>();
    }


    /// <summary>
    /// 플레이어의 상호작용 팝업 UI를 활성화 하는 함수
    /// </summary>
    /// <param name="text_">상호작용 팝업 UI에 작성할 텍스트</param>
    /// <param name="active_">True : 활성화, false : 비활성화</param>
    public void PopupUIActive(string text_, bool active_)
    {
        _uiManagerCs.InteractionPopupUIActive(text_, active_);
    }

    /// <summary>
    /// 플레이어의 돈을 관리하는 함수
    /// </summary>
    /// <param name="money_">+ 또는 - 의 값</param>
    /// <returns>성공시 True, 실패시 False</returns>
    public bool IsPayMoney(int money_)
    {
        return _uiManagerCs.PlayerMoneyControl(money_);
    }



}
