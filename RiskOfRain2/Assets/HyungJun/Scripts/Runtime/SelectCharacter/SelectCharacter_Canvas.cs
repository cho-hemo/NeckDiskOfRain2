using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter_Canvas : MonoBehaviour
{
    public void ReadyBtnClick()
    {
        GioleFunc.LoadScene(GioleData.GOLEM_PLAINS_SCENE_NAME);
    }

    public void BackBtnClick()
    {
        GioleFunc.LoadScene(GioleData.TITLE_SCENE_NAME);
    }
}
