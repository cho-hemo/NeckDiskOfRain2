using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiManager : MonoBehaviour
{
    private GameObject _popMenuObj = default;
    private GameObject _timerObj = default;

    private GameObject _hpBarObj = default;
    private GameObject _hpBarTxtObj = default;

    private GameObject _expBarObj = default;
    private GameObject _expLevelTxtObj = default;


    private bool _popMenuIsOpen = false;

    [SerializeField]
    private int _playerCurrentHp = 0;
    private int _playerMaxHp = 0;


    [SerializeField]
    private int _playerCurrentExp = 0;
    private int _playerMaxExp = 0;
    private int _playerLevel = 1;


    private float _times = 0f;
    private float _hpBarValue = 0f;
    private float _expBarValue = 0f;

    private void Awake()
    {
        // Init Var
        _popMenuObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("PopUpMenu");
        _timerObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("TimerTxt");
        _hpBarObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("HpBar");
        _hpBarTxtObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("HpTxtNum");
        _expBarObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("ExpBar");
        _expLevelTxtObj = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME).FindChildObj("ExpTxtNum");

        // 플레이어 최대 경험치 설정
        _playerMaxExp = 12;
        _playerCurrentExp = 0;

        // 플레이어 최대 체력 설정
        _playerMaxHp = 110;
        _playerCurrentHp = _playerMaxHp;

        // Setting Var
        _popMenuObj.SetActive(false);
    }

    private void Update()
    {
        _times += Time.deltaTime;
        _timerObj.SetTmpText(_times.ToString("00:00.00"));

        // 현재 체력바 반영
        _hpBarTxtObj.SetTmpText($"{_playerCurrentHp}/{_playerMaxHp}");
        _hpBarValue = (float)_playerCurrentHp / (float)_playerMaxHp;
        _hpBarObj.FilledImageControll(_hpBarValue);

        // 현재 경험치바 반영
        _expLevelTxtObj.SetTmpText($"레벨: {_playerLevel}");
        _expBarValue = (float)_playerCurrentExp / (float)_playerMaxExp;
        _expBarObj.FilledImageControll(_expBarValue);



        // 플레이어가 ESC키를 누르면 팝업메뉴가 나온다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_popMenuObj.activeSelf == false)
            {
                _popMenuObj.SetActive(true);
            }
            else if (_popMenuObj.activeSelf)
            {
                _popMenuObj.SetActive(false);
            }
        }
    }


    /// <summary>
    /// 플레이어의 경험치를 올려주고 최대 경험치량에 도달할 경우 레벨 업 함수
    /// </summary>
    /// <param name="expValue_">+ 의 추가 값</param>
    public void PlayerExpPlus(int expValue_)
    {
        _playerCurrentExp += expValue_;
        if (_playerMaxExp <= _playerCurrentExp)
        {
            _playerMaxExp += _playerLevel * 5;
            _playerCurrentExp = 0;
            _playerLevel += 1;
        }
        else { /* Do nothing */ }
    }


    /// <summary>
    /// 플레이어의 Hp를 컨드롤하는 함수
    /// </summary>
    /// <param name="hpValue_">+ 또는 - 의 조정 값</param>
    public void PlayerHpControl(int hpValue_)
    {
        _playerCurrentHp += hpValue_;
    }








}
