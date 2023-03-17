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

    public static void ImageColorControll(this GameObject imageObj_, float r, float g, float b)
    {
        Image targetImage = imageObj_.GetComponentMust<Image>();
        targetImage.color = new Color(r, g, b);
    }
}
