using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using RiskOfRain2.Manager;
using RiskOfRain2.Player;

public class ButtonUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private GameObject _highLightBoxObj = default;
	private GameObject _highLightImageObj = default;
	private RectTransform _highLightRect = default;

	private static List<GameObject> _pickUpList = new List<GameObject>();
	private static List<GameObject> _pickUpInfoList = new List<GameObject>();
	private static List<GameObject> _selectDifficultyList = new List<GameObject>();

	// private static PlayerBase _selectPlayer = default;		// 2023-03-29 / HyungJun / 나중에 캐릭터 추가 할 경우 사용할 변수


	private bool _outMouse = false;

	private static bool _objMoving = false;


	private void Awake()
	{
		_highLightBoxObj = gameObject.FindChildObj("HighlightBox");
		_highLightImageObj = gameObject.FindChildObj("HighlightImage");

		_highLightRect = _highLightBoxObj.GetRect();

		GameObject pickImage_ = gameObject.FindChildObj("PickImage");
		GameObject informationObj_ = transform.parent.gameObject.FindChildObj("BtnInfromation");
		if (pickImage_ != default) { pickImage_.SetActive(false); }
		else if (informationObj_ != default) { informationObj_.SetActive(false); }
		else if (pickImage_ == default || informationObj_ == default)
		{
			/* nothing */
		}


		_highLightBoxObj.SetActive(false);
		_highLightImageObj.SetActive(false);
	}


	/// <summary>
	/// 마우스가 들어왔을때
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerEnter(PointerEventData eventData)
	{
		_highLightBoxObj.SetActive(true);
		_highLightImageObj.SetActive(true);
		_outMouse = false;

		StartCoroutine(HighLightBoxZoomOut());
	}

	/// <summary>
	/// 마우스가 나갈 때
	/// </summary>
	/// <param name="eventData"></param>
	public void OnPointerExit(PointerEventData eventData)
	{
		_highLightBoxObj.SetActive(false);
		_highLightImageObj.SetActive(false);
		_outMouse = true;
	}

	/// <summary>
	/// 하이라이트 애니메이션을 재생하는 코루틴 함수
	/// </summary>
	/// <returns></returns>
	protected IEnumerator HighLightBoxZoomOut()
	{
		float time_ = 0f;
		float scale_ = 0f;
		while (true)
		{
			if (_outMouse) yield break;
			if (1f <= time_) yield break;
			yield return new WaitForSecondsRealtime(0.01f);
			time_ += 0.2f;
			scale_ = EaseInBack(1.1f, 1f, time_);
			_highLightRect.localScale = new Vector3(scale_, scale_, 1);
		}
	}


	protected float EaseInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1;
		float s = 1.70158f;
		return end * (value) * value * ((s + 1) * value - s) + start;
	}


	public void OnPointerClick(PointerEventData eventData)
	{
		_highLightBoxObj.SetActive(false);
		_highLightImageObj.SetActive(false);
	}



	/// <summary>
	/// 캐릭터 선택 함수
	/// </summary>
	public void SelectCharacter()
	{
		// 캐릭터가 선택되면 정보 창의 버튼 색깔 및 설명을 바꿔주는 함수
		ChangeInformaion();

		// Debug.Log(_selectPlayer);
		// _selectPlayer = GetComponent<PlayerBase>();
		// Debug.Log(_selectPlayer);

		// 애니메이션 로직
		foreach (GameObject obj_ in _pickUpList)
		{
			obj_.SetActive(false);
		}
		_pickUpList.Clear();

		GameObject pickUpObj_ = gameObject.FindChildObj("PickImage");
		_pickUpList.Add(pickUpObj_);
		pickUpObj_.SetActive(true);
	}


	/// <summary>
	/// 캐릭터의 정보를 바꿔주는 함수
	/// </summary>
	private void ChangeInformaion()
	{
		// Skill playerSkills_ =
		// PlayerBase

		// switch (gameObject.name)
		// {
		// 	case "Commando":
		// 		// 정보를 바꿔주는 함수

		// 		break;
		// }
	}



	/// <summary>
	/// 캐릭터 선택창에서의 정보 버튼 클릭 함수
	/// </summary>
	public void SelectInformaion()
	{
		foreach (GameObject obj_ in _pickUpInfoList)
		{
			obj_.SetActive(false);
		}
		_pickUpInfoList.Clear();

		GameObject pickUpObj_ = gameObject.FindChildObj("PickImage");
		GameObject informationObj_ = transform.parent.gameObject.FindChildObj("BtnInfromation");


		_pickUpInfoList.Add(informationObj_);
		_pickUpInfoList.Add(pickUpObj_);
		pickUpObj_.SetActive(true);
		informationObj_.SetActive(true);
	}       // SelectInformaion()

	/// <summary>
	/// 게임의 난이도를 조정하는 함수	
	/// </summary>
	/// <param name="num"></param>
	public void SelectDifficulty(int num)
	{
		if (_objMoving) { return; }
		_objMoving = true;
		foreach (GameObject obj_ in _selectDifficultyList)
		{
			obj_.SetActive(false);
		}
		_selectDifficultyList.Clear();




		GameObject pickUpObj_ = gameObject.FindChildObj("PickImage");
		_selectDifficultyList.Add(pickUpObj_);
		pickUpObj_.SetActive(true);


		RectTransform iconListObj_ = transform.parent.gameObject.GetRect();

		switch (num)
		{
			case 1:     // 쉬움
				GameManager.Instance.GameDiffi = Difficulty.EASY;
				StartCoroutine(MoveObject(70f, iconListObj_));
				break;
			case 2:     // 중간
				GameManager.Instance.GameDiffi = Difficulty.NORMAL;
				StartCoroutine(MoveObject(0f, iconListObj_));
				break;
			case 3:     // 어려움
				GameManager.Instance.GameDiffi = Difficulty.HARD;
				StartCoroutine(MoveObject(-70f, iconListObj_));
				break;
		}

	}       // SelectDifficulty()

	/// <summary>
	/// 수평선으로 오브젝트를 움직이는 함수
	/// </summary>
	/// <param name="moveValue_"></param>
	/// <param name="obj_"></param>
	/// <returns></returns>
	private IEnumerator MoveObject(float moveValue_, RectTransform obj_)
	{
		float controlValue_ = 0f;
		float time_ = 0f;
		int correction = 0;

		if (moveValue_ < 0)
		{
			controlValue_ = -1f;
			correction--;
		}
		if (moveValue_ > 0)
		{
			controlValue_ = 1f;
			correction++;
		}
		if (moveValue_ == 0)
		{
			// 난이도가 Easy 일때
			if (obj_.anchoredPosition.x == 0f)
			{
				/* Do nothing */
			}
			else if (0f < obj_.anchoredPosition.x)
			{
				controlValue_ = -1f;
			}
			else if (obj_.anchoredPosition.x < 0f)
			{
				controlValue_ = 1f;
			}
		}

		while (true)
		{
			yield return new WaitForSeconds(0.01f);
			time_ += 0.01f;

			controlValue_ = EaseOutExpo(obj_.anchoredPosition.x, moveValue_ + correction, time_);
			obj_.anchoredPosition = new Vector2(controlValue_, 0f);

			// obj_.localPosition = new Vector3(movingValue_, 0f, 0f);

			if ((int)moveValue_ == (int)obj_.anchoredPosition.x)
			{
				_objMoving = false;
				yield break;
			}
		}       // While()
	}       // MoveObject()

	private float EaseOutExpo(float start, float end, float value)
	{
		end -= start;
		return end * (-Mathf.Pow(2, -10 * value) + 1) + start;
	}


}
