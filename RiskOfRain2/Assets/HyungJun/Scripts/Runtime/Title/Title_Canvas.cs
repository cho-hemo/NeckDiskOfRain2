using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Title_Canvas : MonoBehaviour
{

    public void StartGame_SingleBtnClick()
    {
        GioleFunc.LoadScene(GioleData.SELECT_CHARACTER_SCENE_NAME);
    }

    public void ExitGameBtnClick()
    {
        GioleFunc.QuitThisGame();
    }

}
