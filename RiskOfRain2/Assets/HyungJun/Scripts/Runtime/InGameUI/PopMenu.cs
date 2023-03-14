using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopMenu : MonoBehaviour
{
    // 인 게임에서 ESC 키를 누르면 나오는 메뉴를 관리하는 스크립트


    // 팝업창이 나오면 게임 스탑
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    // 팝업창이 꺼지면 게임 계속
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // 계속하기를 클릭할 경우
    public void ContinueBtnClick()
    {
        gameObject.SetActive(false);
    }

    // 타이틀로 돌아가기
    public void BackToTitleBtnClick()
    {
        GioleFunc.LoadScene(GioleData.TITLE_SCENE_NAME);
    }

    // 데스크톱으로 돌아가기
    public void ExitGameBtnClick()
    {
        GioleFunc.QuitThisGame();
    }
}
