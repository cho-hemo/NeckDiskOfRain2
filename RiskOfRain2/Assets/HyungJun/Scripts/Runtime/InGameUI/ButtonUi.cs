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

	private bool _outMouse = false;

	private void Awake()
	{
		_highLightBoxObj = gameObject.FindChildObj("HighlightBox");
		_highLightImageObj = gameObject.FindChildObj("HighlightImage");

		_highLightRect = _highLightBoxObj.GetRect();

		// if (gameObject.FindChildObj("PickImage") == default)
		// {
		// 	/* nothing */
		// }
		// else
		// {
		// 	_pickUpObj = gameObject.FindChildObj("PickImage");
		// }


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
	/// 캐릭터를 선택하면 리스트에 해당 오브젝트를 넣고 선택하는 함수
	/// </summary>
	public void SelectCharacter()
	{
		foreach (GameObject obj_ in _pickUpList)
		{
			obj_.SetActive(false);
		}
		_pickUpList.Clear();

		GameObject pickUpObj_ = gameObject.FindChildObj("PickImage");
		_pickUpList.Add(pickUpObj_);
		pickUpObj_.SetActive(true);
	}

	// public delegate C123haracterInfo();


}
