using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	private GameObject _highLightBoxObj = default;
	private GameObject _highLightImageObj = default;
	private RectTransform _highLightRect = default;

	private static List<GameObject> _pickUpList = new List<GameObject>();
	private static List<GameObject> _pickUpInfoList = new List<GameObject>();

	private bool _outMouse = false;

	private void Awake()
	{
		_highLightBoxObj = gameObject.FindChildObj("HighlightBox");
		_highLightImageObj = gameObject.FindChildObj("HighlightImage");

		_highLightRect = _highLightBoxObj.GetRect();

		GameObject pickImage_ = gameObject.FindChildObj("PickImage");
		GameObject informationObj_ = transform.parent.gameObject.FindChildObj("BtnInfromation");
		if (pickImage_ == default || informationObj_ == default)
		{
			/* nothing */
		}
		else if (pickImage_ != default || informationObj_ != default)
		{
			pickImage_.SetActive(false);
			informationObj_.SetActive(false);
		}


		_highLightBoxObj.SetActive(false);
		_highLightImageObj.SetActive(false);
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		_highLightBoxObj.SetActive(true);
		_highLightImageObj.SetActive(true);
		_outMouse = false;

		StartCoroutine(HighLightBoxZoomOut());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_highLightBoxObj.SetActive(false);
		_highLightImageObj.SetActive(false);
		_outMouse = true;
	}

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
			scale_ = EaseInBack(1.2f, 1f, time_);
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

	private void ChangeInformaion()
	{
		switch (gameObject.name)
		{
			case "Commando":
				// 정보를 바꿔주는 함수

				break;
		}
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
	}


}
