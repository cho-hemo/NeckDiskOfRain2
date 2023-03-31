using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using RiskOfRain2.Manager;
using RiskOfRain2.Player;


public partial class PlayerUiManager : MonoBehaviour, IObserver
{
	// { Debug Mode

	// public float BarMoveSpeed = 0.3f;
	public void Debug_MoneyBtn(int value_)
	{
		PlayerMoneyControl(value_);
	}
	public void Debug_RunaCoinBtn(int value_)
	{
		PlayerLunaCoinControl(value_);
	}
	public void Debug_SkillActiveBtn(int num_)
	{
		PlayerSkillActiveIcon(num_, 4);
	}
	public void Debug_MonsterHpBtn()
	{
		UIManager.Instance.MonsterHpBarControl("Cube", 40, 10);
	}
	// } Debug Mode

	#region 게임오브젝트
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
	private GameObject _lunaCoinTxtObj = default;


	private GameObject _interactionPopupObj = default;

	private GameObject _scoreBoardObj = default;
	private GameObject _crossHair = default;



	// 보스 게임 오브젝트
	private GameObject _bossUiObj = default;
	private GameObject _bossHpbarObj = default;
	private GameObject _bossHpbarTxtObj = default;


	private GameObject _chatLineObj = default;

	// 미션 UI 오브젝트
	private GameObject _missionUiObj = default;

	#endregion 게임오브젝트

	private PlayerBase _playerInfo = default;


	// 플레이어의 스코어보드의 아이템 리스트
	public List<GameObject> ItemList = new List<GameObject>();
	private GameObject _ItemListObj = default;




	private List<GameObject> _skillList = new List<GameObject>();
	private List<bool> _isSkillActivation = new List<bool>();
	private List<int> _skillStackList = new List<int>();




	[SerializeField]
	private int _playerCurrentHp = 0;
	private int _playerMaxHp = 0;

	private int _bossCurrentHp = 0;
	private int _bossMaxHp = 0;


	[SerializeField]
	private int _playerCurrentExp = 0;
	private int _playerMaxExp = 0;

	// 난이도 바의 움직이는 속도
	// private float _barMoveSpeed = 0.3f;
	private int _levelBarMoveValue = 1;


	/// <summary>몬스터의 레벨 변수</summary>
	public int MonsterLevel { get; private set; } = 1;

	/// <summary>스테이지 레벨 변수</summary>
	public int StageLevel { get; private set; } = 1;

	/// <summary>플레이어 레벨을 반환하는 프로퍼티</summary>
	public int PlayerLevel { get; private set; } = 1;

	public int PlayerMoney { get; private set; } = 100;
	public int PlayerLunaCoin { get; private set; } = 0;

	/// <summary>난이도를 설정하는 변수</summary>
	public Difficulty InGameDifficulty = Difficulty.NONE;


	private float _times = 0f;
	private float _hpBarValue = 0f;
	private float _bossHpBarValue = 0f;
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
		_lunaCoinTxtObj = uiObj_.FindChildObj("LunaCoinText");

		_interactionPopupObj = uiObj_.FindChildObj("InteractionPopupUI");

		_bossUiObj = uiObj_.FindChildObj("BossUI");
		_bossHpbarTxtObj = uiObj_.FindChildObj("BossHpTxt");

		_chatLineObj = uiObj_.FindChildObj("ChatBoxUI").FindChildObj("ChatLine");
		_bossHpbarObj = uiObj_.FindChildObj("BossHpBar");

		_scoreBoardObj = uiObj_.FindChildObj("ScoreBoardUI");
		_crossHair = uiObj_.FindChildObj("CrossHair");

		_missionUiObj = uiObj_.FindChildObj("MissionUI");
		_ItemListObj = uiObj_.FindChildObj("ItemListPanel");



		StageLevel = 1;

		// 플레이어 최대 경험치 설정
		_playerMaxExp = 12;
		_playerCurrentExp = 0;
		PlayerLevel = 1;

		// 플레이어 최대 체력 설정
		_playerMaxHp = 110;
		_playerCurrentHp = _playerMaxHp;

		// 보스 최대 체력 설정
		// _bossMaxHp = 여기서 보스의 체력을 동기화
		// _bossCurrentHp = _bossMaxHp;

		// Setting Instance
		_popMenuObj.SetActive(false);
		_interactionPopupObj.SetActive(false);
		_bossUiObj.SetActive(false);
		_chatLineObj.SetActive(false);
		_scoreBoardObj.SetActive(false);

		foreach (Transform obj_ in _levelIconObj.transform)
		{
			obj_.gameObject.SetActive(false);
		}

		// 게임의 난이도에 따라서 아이콘을 변경하는 로직
		switch (GameManager.Instance.GameDiffi)
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



		PlayerMoneyControl(0);
		PlayerLunaCoinControl(0);

		StartCoroutine(LevelBarController());
		// StartCoroutine(LevelBarMove());

	}

	private void Start()
	{
		_playerInfo = GameManager.Instance.Player;

		GameObject uiObj_ = GioleFunc.GetRootObj(GioleData.PLAYER_UI_CANVAS_OBJ_NAME);

		GameObject skillObj_ = default;
		GameObject skillCount_ = default;
		// 스킬 리스트에 스킬 담는 로직
		for (int i = 0; i < 5; i++)
		{
			skillObj_ = uiObj_.FindChildObj("SkillUI").FindChildObj($"Skill{i}");
			_skillList.Add(skillObj_);
			skillObj_.FindChildObj("CoolDown").SetActive(false);
		}

		// 스킬 스택을 초기화하는 로직
		int index = 0;
		foreach (var value_ in _playerInfo.Skills)
		{
			skillCount_ = uiObj_.FindChildObj($"Skill{index}").FindChildObj("SkillCostTxt");
			if (value_.SkillMaxStack == 1)
			{
				skillCount_.SetActive(false);
			}
			index++;
		}

		PlayerHpControl((int)_playerInfo.CurrentHp, (int)_playerInfo.MaxHp);        // 플레이어 Hp UI 초기화
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

		if (Input.GetKeyDown(KeyCode.Return) && !_popMenuObj.activeSelf)
		{
			if (!_chatLineObj.activeSelf)
			{
				_chatLineObj.SetActive(true);
			}
			else if (_chatLineObj.activeSelf)
			{
				_chatLineObj.SetActive(false);
			}
		}

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			ScoreBoardPopup(true);

		}
		else if (Input.GetKeyUp(KeyCode.Tab))
		{
			ScoreBoardPopup(false);
		}

	}       // Update()

	/// <summary>
	/// 탭 키를 누를경우 나오는 점수화면 호출하는 함수
	/// </summary>
	/// <param name="popupCheck">true : 켜주기 false : 꺼주기</param>
	public void ScoreBoardPopup(bool popupCheck)
	{
		_scoreBoardObj.SetActive(popupCheck);
		_crossHair.SetActive(!popupCheck);
	}


	#region  플레이어 관련 함수
	public void UpdateDate(object data) {/* Do nothing */}

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

	}

	/// <summary>
	/// 플레이어의 정보를 갱신해주는 함수
	/// </summary>
	public void UpdateDate()
	{
		PlayerHpControl((int)_playerInfo.CurrentHp, (int)_playerInfo.MaxHp);
		PlayerExpSync(_playerInfo.Level, _playerInfo.CurrentExp, _playerInfo.MaxExp);
	}

	/// <summary>
	/// 플레이어 경험치를 가져와서 UI에 동기화 하는 로직
	/// </summary>
	/// <param name="level_"></param>
	/// <param name="currentExp_"></param>
	/// <param name="maxExp_"></param>
	public void PlayerExpSync(int level_, int currentExp_, int maxExp_)
	{
		// 현재 경험치바 반영
		_expLevelTxtObj.SetTmpText($"레벨: {level_}");
		float expBarValue_ = (float)currentExp_ / (float)maxExp_;
		_expBarObj.FilledImageControll(expBarValue_);
	}

	/// <summary>
	/// 플레이어의 Hp를 컨드롤하는 함수
	/// </summary>
	/// <param name="hpValue_">+ 또는 - 의 조정 값</param>
	public void PlayerHpControl(int currentHp_, int maxHp_)
	{
		// 현재 체력바 반영
		_hpBarTxtObj.SetTmpText($"{currentHp_}/{maxHp_}");
		_hpBarValue = (float)currentHp_ / (float)maxHp_;
		_hpBarObj.FilledImageControll(_hpBarValue);
	}

	/// <summary>
	/// 플레이어의 돈을 관리하는 함수
	/// </summary>
	/// <param name="moneyValue_">+ 또는 - 의 조정할 값</param>
	/// <returns>성공시 true 실패시 false</returns>
	public bool PlayerMoneyControl(int moneyValue_)
	{
		if (moneyValue_ < 0)
		{
			if (PlayerMoney < Mathf.Abs(moneyValue_))
			{
				Debug.Log("[PlayerUiManager] PlayerMoneyControl : 플레이어의 머니가 부족합니다.");
				return false;
			}
			else
			{
				PlayerMoney += moneyValue_;
				_moneyTxtObj.SetTmpText(PlayerMoney.ToString());
				return true;
			}
		}
		else
		{
			PlayerMoney += moneyValue_;
			_moneyTxtObj.SetTmpText(PlayerMoney.ToString());
			return true;
		}
	}       // PlayerMoneyControl()

	/// <summary>
	/// 플레이어의 루나코인을 관리하는 함수
	/// </summary>
	/// <param name="coinValue_">+ 또는 - 의 조정할 값</param>
	/// <returns>성공시 true 실패시 false</returns>
	public bool PlayerLunaCoinControl(int coinValue_)
	{
		_lunaCoinTxtObj.SetTmpText(PlayerLunaCoin.ToString());
		if (coinValue_ < 0)
		{
			if (PlayerLunaCoin < Mathf.Abs(coinValue_))
			{
				Debug.Log("[PlayerUiManager] PlayerLunaCoinControl : 플레이어의 루나코인이 부족합니다.");
				return false;
			}
			else
			{
				PlayerLunaCoin -= coinValue_;
				return true;
			}
		}
		else
		{
			PlayerLunaCoin += coinValue_;
			return true;
		}
	}

	/// <summary>
	/// 스킬을 발동하는 함수
	/// </summary>
	/// <param name="num">발동 시킬 스킬의 넘버 0 ~ 4</param>
	/// <param name="coolTime_">설정할 쿨타임</param>
	public void PlayerSkillActiveIcon(int num, float coolTime_)
	{
		StartCoroutine(SkillActive(num, coolTime_));
	}
	#endregion  플레이어 관련 함수


	/// <summary>
	/// 팝업 UI를 관리하는 함수
	/// </summary>
	/// <param name="text_"></param>
	/// <param name="active_"></param>
	public void InteractionPopupUIActive(string text_, bool active_)
	{
		if (active_)
		{
			_interactionPopupObj.SetActive(true);
			_interactionPopupObj.FindChildObj("InteractionTxt").SetTmpText(text_);
		}
		else
		{
			_interactionPopupObj.SetActive(false);
		}
	}



	#region 보스 UI 관련 로직
	/// <summary>보스 UI를 켜주는 함수</summary>
	public void ActiveBossUi()
	{
		_bossUiObj.SetActive(true);
	}
	/// <summary>
	/// 보스의 UI를 설정하고 보여주는 함수
	/// </summary>
	/// <param name="name_">보스이름</param>
	/// <param name="secondName_">보스의 두번쨰 이름</param>
	public void SetBossUi(string name_, string secondName_)
	{
		// BossHpControl(0);
		_bossUiObj.FindChildObj("BossNameTxt").SetTmpText(name_);

		_bossUiObj.FindChildObj("BossSecondNameTxt").SetTmpText("<sprite name=\"CloudLeft\">" + secondName_ + "<sprite name=\"CloudRight\">");
	}       // SetBossUi

	/// <summary>
	/// 보스의 체력바를 컨트롤 하는 함수
	/// </summary>
	/// <param name="hpValue_"></param>
	public void BossHpControl(int bossCurrentHp_, int bossMaxHp_)
	{
		// 현재 체력바 반영
		_bossHpbarTxtObj.SetTmpText($"{bossCurrentHp_}/{bossMaxHp_}");
		_bossHpBarValue = (float)bossCurrentHp_ / (float)bossMaxHp_;

		// Hp바의 fill 갑을 조절하는 함수
		_bossHpbarObj.FilledImageControll(_bossHpBarValue);
		if (bossCurrentHp_ <= 0)
		{
			_bossUiObj.SetActive(false);
		}       // if: 보스의 체력이 0 이하면 Ui를 꺼준다.
	}       // BossHpControl()
	#endregion 보스 UI 관련 로직

	/// <summary>
	/// 미션 UI를 체크해주는 함수
	/// </summary>
	public void CheckMissionUiComplate()
	{
		_missionUiObj.FindChildObj("CheckBoxImage").SetActive(false);
		_missionUiObj.FindChildObj("CheckBoxComeplateImage").SetActive(true);
		_missionUiObj.FindChildObj("MissionTxt").SetFontStyle(TMPro.FontStyles.Strikethrough);
		_missionUiObj.FindChildObj("MissionTxt").SetFontColor(0.5f, 0.5f, 0.5f);
	}

	// 아이템 리스트에 아이템을 추가하고 스코어보드의 아이템에 아이템 이미지를 추가하는 함수
	public void AddItemList(GameObject obj_)
	{
		ItemList.Add(obj_);
		Instantiate(obj_, _ItemListObj.transform);

	}


}       // Class PlayerUiManager
