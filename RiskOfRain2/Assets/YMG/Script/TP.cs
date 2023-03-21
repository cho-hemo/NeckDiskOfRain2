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
        // GetAxisRaw : Axis 값을 정수로 반환하는 함수
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        // normalized 방향값은 1
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec* speed*Time.deltaTime;

        anime.SetBool("isRun", moveVec != Vector3.zero);

        // LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
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
