using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUiManager : MonoBehaviour
{
    // { Devlope Mode Var

    // public float BarMoveSpeed = 0.3f;

    // } Devlope Mode Var

    private GameObject _popMenuObj = default;
    private GameObject _timerObj = default;
    private GameObject _timermillSecondObj = default;

    private GameObject _hpBarObj = default;
    private GameObject _hpBarTxtObj = default;

    private GameObject _expBarObj = default;
    private GameObject _expLevelTxtObj = default;

    private GameObject _levelIconObj = default;

    private GameObject _levelBarObj = default;

    private GameObject _stageLevelTxtObj = default;
    private GameObject _monsterLevelTxtObj = default;

    private GameObject _moneyTxtObj = default;
    private GameObject _runaCoinTxtObj = default;



    private bool _popMenuIsOpen = false;

    [SerializeField]
    private int _playerCurrentHp = 0;
    private int _playerMaxHp = 0;

    [SerializeField]
    private int _playerCurrentExp = 0;
    private int _playerMaxExp = 0;

    // 난이도 바의 움직이는 속도
    private float _barMoveSpeed = 0.3f;
    private int _levelBarMoveValue = 1;


    /// <summary>몬스터의 레벨 변수</summary>
    public int MonsterLevel { get; private set; } = 1;

    /// <summary>스테이지 레벨 변수</summary>
    public int StageLevel { get; private set; } = 1;

    /// <summary>플레이어 레벨을 반환하는 프로퍼티</summary>
    public int PlayerLevel { get; private set; } = 1;

    public int PlayerMoney { get; private set; } = 0;
    public int PlayerRunaCoin { get; private set; } = 0;

    /// <summary>난이도를 설정하는 변수</summary>
    public Difficulty InGameDifficulty = Difficulty.NONE;


    private float _times = 0f;
    private float _hpBarValue = 0f;
    private float _expBarValue = 0f;


    private void Awake()
    {
        // { DebugMode
        // Time.timeScale = 10f;
        // } DebugMode



        GameObject uiObj_ = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME);

        // Init Instance
        _popMenuObj = uiObj_.FindChildObj("PopUpMenu");
        _timerObj = uiObj_.FindChildObj("TimerTxt");
        _timermillSecondObj = uiObj_.FindChildObj("TimerMiliSecond");

        _hpBarObj = uiObj_.FindChildObj("HpBar");
        _hpBarTxtObj = uiObj_.FindChildObj("HpTxtNum");
        _expBarObj = uiObj_.FindChildObj("ExpBar");
        _expLevelTxtObj = uiObj_.FindChildObj("ExpTxtNum");
        _levelIconObj = uiObj_.FindChildObj("Level_Icon");
        _levelBarObj = uiObj_.FindChildObj("LevelBar");

        _stageLevelTxtObj = uiObj_.FindChildObj("StageTxt");
        _monsterLevelTxtObj = uiObj_.FindChildObj("LevelTxt");

        _moneyTxtObj = uiObj_.FindChildObj("MoneyText");
        _runaCoinTxtObj = uiObj_.FindChildObj("LunaCoinText");


        StageLevel = 1;

        // 플레이어 최대 경험치 설정
        _playerMaxExp = 12;
        _playerCurrentExp = 0;
        PlayerLevel = 1;

        // 플레이어 최대 체력 설정
        _playerMaxHp = 110;
        _playerCurrentHp = _playerMaxHp;

        // Setting Instance
        _popMenuObj.SetActive(false);


        foreach (Transform obj_ in _levelIconObj.transform)
        {
            obj_.gameObject.SetActive(false);
        }




        // 게임의 난이도에 따라서 아이콘을 변경하는 로직
        switch (InGameDifficulty)
        {
            case Difficulty.NONE:
                Debug.Log("[PlayerUiManager] Awake : Not Setting Difficulty -> Change to Normal");
                InGameDifficulty = Difficulty.NORMAL;
                _levelIconObj.FindChildObj("Normal").SetActive(true);
                break;
            case Difficulty.EASY:
                _levelIconObj.FindChildObj("Easy").SetActive(true);
                break;
            case Difficulty.NORMAL:
                _levelIconObj.FindChildObj("Normal").SetActive(true);
                break;
            case Difficulty.HARD:
                _levelIconObj.FindChildObj("Hard").SetActive(true);
                break;
        }

        PlayerExpPlus(0);
        PlayerHpControl(0);

        PlayerMoneyControl(0);
        PlayerRunaCoinControl(0);

        StartCoroutine(LevelBarController());
        // StartCoroutine(LevelBarMove());

    }

    private void Update()
    {
        // 타이머 반영
        _times += Time.deltaTime;
        int min = (int)(_times / 60);
        float second = _times % 60;
        _timerObj.SetTmpText(min.ToString("00:") + second.ToString("00.00"));

        _stageLevelTxtObj.SetTmpText("스테이지 " + StageLevel.ToString());
        _monsterLevelTxtObj.SetTmpText("레벨. " + MonsterLevel.ToString());
        // Debug.Log((int)(_times * 10));
        // if ((int)(_times * 10) % 37 == 0)
        // {
        //     Debug.Log("레벨바 벨류 증가!");
        //     _levelBarMoveValue++;
        // }




        // 난이도바 반영
        // _levelBarObj.transform.localPosition += Vector3.left;

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

    private IEnumerator LevelBarController()
    {
        int times_ = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            ++times_;
            if (times_ == 37)
            {
                _levelBarObj.transform.localPosition += Vector3.left;
                times_ = 0;
                if (_levelBarMoveValue % 40 == 0)
                {
                    ++MonsterLevel;
                }
                ++_levelBarMoveValue;
            }
        }
    }

    // private IEnumerator LevelBarMove()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(0.37f);
    //         _levelBarObj.transform.localPosition += Vector3.left * 0.1f;
    //     }
    // }

    /// <summary>
    /// 플레이어의 경험치를 올려주고 최대 경험치량에 도달할 경우 레벨 업 함수
    /// </summary>
    /// <param name="expValue_">+ 의 추가 값</param>
    public void PlayerExpPlus(int expValue_)
    {
        _playerCurrentExp += expValue_;
        if (_playerMaxExp <= _playerCurrentExp)
        {
            _playerMaxExp += PlayerLevel * 5;
            _playerCurrentExp = 0;
            PlayerLevel += 1;
        }
        else { /* Do nothing */ }

        // 현재 경험치바 반영
        _expLevelTxtObj.SetTmpText($"레벨: {PlayerLevel}");
        _expBarValue = (float)_playerCurrentExp / (float)_playerMaxExp;
        _expBarObj.FilledImageControll(_expBarValue);
    }

    /// <summary>
    /// 플레이어의 Hp를 컨드롤하는 함수
    /// </summary>
    /// <param name="hpValue_">+ 또는 - 의 조정 값</param>
    public void PlayerHpControl(int hpValue_)
    {
        _playerCurrentHp += hpValue_;
        // 현재 체력바 반영
        _hpBarTxtObj.SetTmpText($"{_playerCurrentHp}/{_playerMaxHp}");
        _hpBarValue = (float)_playerCurrentHp / (float)_playerMaxHp;
        _hpBarObj.FilledImageControll(_hpBarValue);
    }




    public void Debug_MoneyBtn(int value_)
    {
        PlayerMoneyControl(value_);
    }
    public void Debug_RunaCoinBtn(int value_)
    {
        PlayerRunaCoinControl(value_);
    }


    /// <summary>
    /// 플레이어의 돈을 관리하는 함수
    /// </summary>
    /// <param name="moneyValue_">+ 또는 - 의 조정할 값</param>
    /// <returns>성공시 true 실패시 false</returns>
    public bool PlayerMoneyControl(int moneyValue_)
    {
        _moneyTxtObj.SetTmpText(PlayerMoney.ToString());
        if (moneyValue_ < 0)
        {
            if (PlayerMoney < Mathf.Abs(moneyValue_))
            {
                Debug.Log("[PlayerUiManager] PlayerMoneyControl : 플레이어의 머니가 부족합니다.");
                return false;
            }
            else
            {
                PlayerMoney -= moneyValue_;
                return true;
            }
        }
        else
        {
            PlayerMoney += moneyValue_;
            return true;
        }
    }       // PlayerMoneyControl()

    /// <summary>
    /// 플레이어의 루나코인을 관리하는 함수
    /// </summary>
    /// <param name="coinValue_">+ 또는 - 의 조정할 값</param>
    /// <returns>성공시 true 실패시 false</returns>
    public bool PlayerRunaCoinControl(int coinValue_)
    {
        _runaCoinTxtObj.SetTmpText(PlayerRunaCoin.ToString());
        if (coinValue_ < 0)
        {
            if (PlayerRunaCoin < Mathf.Abs(coinValue_))
            {
                Debug.Log("[PlayerUiManager] PlayerRunaCoinControl : 플레이어의 루나코인이 부족합니다.");
                return false;
            }
            else
            {
                PlayerRunaCoin -= coinValue_;
                return true;
            }
        }
        else
        {
            PlayerRunaCoin += coinValue_;
            return true;
        }
    }

}       // Class PlayerUiManager
