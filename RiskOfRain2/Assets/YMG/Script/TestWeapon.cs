using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    public enum Type { Melee, Range}
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;

    public void Use()
    {
        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range) 
        {
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        // 1
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled= true;
        // 2
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;

        //yield return null; // 1������ ���

    }

    IEnumerator Shot() 
    {
        GameObject intantBullet = Instantiate(bullet,bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50; 
        yield return null;
    }

    // Use() ���� ��ƾ -> Swing() ���� ��ƾ -> Use()
    // Use() ���� ��ƾ + Swing() �ڷ�ƾ (Co-Op)
    // yield ����� ����
}
