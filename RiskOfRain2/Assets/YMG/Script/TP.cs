using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Animator anime;

    GameObject nearObject;

    void Start()
    {
        anime = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // GetAxisRaw : Axis ���� ������ ��ȯ�ϴ� �Լ�
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // normalized ���Ⱚ�� 1
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec* speed*Time.deltaTime;

        anime.SetBool("isRun", moveVec != Vector3.zero);

        // LookAt() : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        transform.LookAt(transform.position + moveVec);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
            nearObject = other.gameObject;
        Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
            nearObject = null;
    }


}
