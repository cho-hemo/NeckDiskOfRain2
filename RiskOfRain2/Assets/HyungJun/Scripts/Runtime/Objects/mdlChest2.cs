using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mdlChest2 : InteractionObjects
{
    private GameObject _neededMoneyObj = default;
    private Animator _chestAni = default;
    private bool _inArea = true;


    public int NeededMoney
    {
        get;
        set;
    } = 15;


    private void Awake()
    {
        // Instance Init
        _neededMoneyObj = gameObject.FindChildObj("NeededMoney");
        _chestAni = GetComponent<Animator>();

        // 상자의 가격을 설정하는 함수
        _neededMoneyObj.FindChildObj("Txt").SetTmpText($"${NeededMoney}");

        // Set Instance
        _neededMoneyObj.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") && !_chestAni.GetBool("Open"))
        {
            // 가격 표시
            _neededMoneyObj.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            // 가격 표시 끄기
            _neededMoneyObj.SetActive(false);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && !_chestAni.GetBool("Open"))
        {
            // 플레이어가 상자와의 거리가 2f 일경우
            if (Vector3.Distance(gameObject.transform.position, other.transform.position) <= 2f)
            {
                if (_inArea)
                {
                    _inArea = false;
                    UIManager.Instance.PopupUIActive(" 상자열기", true);
                }
                else if (Input.GetKeyDown(KeyCode.E) && UIManager.Instance.IsPayMoney(-NeededMoney))
                {
                    _chestAni.SetBool("Open", true);
                    Debug.Log("[mdlChest2] OntriggerStay : 상자 오픈!");
                    _neededMoneyObj.SetActive(false);
                    UIManager.Instance.PopupUIActive("", false);
                }
                // 플레이어와 상자의 거리를 계산해서 적다면 열어주기
            }
            // 상자와의 거리가 0.1f 이상일 경우
            else if (!_inArea)
            {
                _inArea = true;
                UIManager.Instance.PopupUIActive("", false);
            }
            // 가격이 플레이어 보기
            _neededMoneyObj.transform.LookAt(other.transform);
        }
    }       // OnTriggerStay()



}       // class mdlChest2