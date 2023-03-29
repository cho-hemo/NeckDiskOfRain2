using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static partial class GioleFunc
{
	public static Vector2 GetCameraSize()
	{
		Vector2 cameraSize = Vector2.zero;
		cameraSize.y = Camera.main.orthographicSize * 2.0f;
		cameraSize.x = cameraSize.y * Camera.main.aspect;

		return cameraSize;
	}       // GetCameraSize();


	public static void SetTmpText(this GameObject obj_, string text_)
	{
		//! 텍스트매쉬프로 형태의 컴포넌트의 텍스트를 설정하는 경우
		TMP_Text tmpTXT = obj_.GetComponent<TMP_Text>();
		if (tmpTXT == null || tmpTXT == default)
		{
			return;
		}       // if: 가져올 텍스트매쉬 컴포넌트가 없는 경우 리턴
				// 가져올 텍스트매쉬 컴포넌트가 존재하는 경우
		tmpTXT.text = text_;
	}       // SetTextMeshPro()

	/// <summary>
	/// 이미지의 타입이 Filled 인 상태이고 해당 양을 조정한다.
	/// </summary>
	/// <param name="imageObj_"></param>
	/// <param name="fValue_">0 ~ 1 의 조정할 float 값</param>
	public static void FilledImageControll(this GameObject imageObj_, float fValue_)
	{
		Image targetImage = imageObj_.GetComponent<Image>();
		if (targetImage.type == Image.Type.Filled)
		{
			targetImage.fillAmount = fValue_;
		}
		else
		{
			Debug.Log($"[{imageObj_.name}]해당 이미지는 이미지 타입이 Filled가 아닙니다.");
			return;
		}
	}       // FilledImageCortroll()

	/// <summary>
	/// 폰트의 글꼴을 바꿔주는 함수
	/// </summary>
	public static void SetFontStyle(this GameObject textObj_, FontStyles styles)
	{
		textObj_.GetComponent<TMP_Text>().fontStyle = styles;
	}


	public static void SetFontColor(this GameObject textObj_, float r, float g, float b)
	{
		textObj_.GetComponent<TMP_Text>().color = new Color(r, g, b);
	}


	/// <summary>
	/// 이미지의 색상을 설정하는 함수
	/// </summary>
	/// <param name="imageObj_">해당 이미지 게임오브젝트</param>
	/// <param name="r">0 ~ 1의 레드값</param>
	/// <param name="g">0 ~ 1의 그린값</param>
	/// <param name="b">0 ~ 1의 블루값</param>
	public static void SetImageColor(this GameObject imageObj_, float r, float g, float b)
	{
		Image targetImage = imageObj_.GetComponentMust<Image>();
		targetImage.color = new Color(r, g, b);
	}       // ImageColorControll()


	/// <summary>
	/// 이미지의 색상을 설정하는 함수
	/// </summary>
	/// <param name="imageObj_">해당 이미지 게임오브젝트</param>
	/// <param name="r">0 ~ 1의 레드값</param>
	/// <param name="g">0 ~ 1의 그린값</param>
	/// <param name="b">0 ~ 1의 블루값</param>
	/// <param name="a">0 ~ 1의 알파값</param>
	public static void SetImageColor(this GameObject imageObj_, float r, float g, float b, float a)
	{
		Image targetImage = imageObj_.GetComponentMust<Image>();
		targetImage.color = new Color(r, g, b, a);
	}       // ImageColorControll()


	/// <summary>
	/// 스프라이트를 넣으면 이미지 오브젝트가 붙어있는 게임 오브젝트로 반환하는 함수
	/// </summary>
	/// <param name="sprite_"></param>
	/// <returns></returns>
	public static GameObject SpriteToImage(Sprite sprite_)
	{
		GameObject resultObj_ = new GameObject();
		resultObj_.AddComponent<Image>();
		Image image_ = resultObj_.GetComponent<Image>();
		image_.sprite = sprite_;

		return resultObj_;
	}


}
