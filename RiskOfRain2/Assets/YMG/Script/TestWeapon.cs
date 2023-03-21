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

        //yield return null; // 1프레임 대기

    }

    IEnumerator Shot() 
    {
        GameObject intantBullet = Instantiate(bullet,bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50; 
        yield return null;
    }

    // Use() 메인 루틴 -> Swing() 서브 루틴 -> Use()
    // Use() 메인 루틴 + Swing() 코루틴 (Co-Op)
    // yield 결과를 전달
}
