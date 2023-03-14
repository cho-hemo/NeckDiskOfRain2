using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUi_Canvas : MonoBehaviour
{
    private GameObject _popMenu = default;

    private bool _popMenuIsOpen = false;

    private void Awake()
    {
        // Init Var
        _popMenu = gameObject.FindChildObj(GioleData.POPUPMENU_OBJ_NAME);

        // Setting Var
        _popMenu.SetActive(false);
    }

    private void Update()
    {
        // 플레이어가 ESC키를 누르면 팝업메뉴가 나온다.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_popMenu.activeSelf == false)
            {
                _popMenu.SetActive(true);
            }
            else if (_popMenu.active)
            {
                _popMenu.SetActive(false);
            }

        }
    }
}
