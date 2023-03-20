using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private bool _isActive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && _isActive)
        {
            UIManager.Instance.PopupUIActive(" 텔레포터 가동..?", true);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            _isActive = false;
            UIManager.Instance.PopupUIActive("", false);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            UIManager.Instance.PopupUIActive("", false);
        }
    }





}
