using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // LayerMask � ������Ʈ, � ���̾ �߻�ü�� �浹���� ����
    public LayerMask collisionMask;
    float speed = 10;

    public void SetSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    void Update() // ����ĳ��Ʈ ���
    { // �浹 ���� collision
        // ������ �̵��� �Ÿ��� �浹�� ���� ���
        float moveDistance = speed * Time.deltaTime; // �����ӿ��� �̵��� �Ÿ�
        CheckCollisions (moveDistance); // ���ο� �ż���
        transform.Translate (Vector3.forward * moveDistance);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //float moveDistance_ = speed * Time.deltaTime;
    //    float moveDistance_ = 10.0f;

    //    Ray ray = new Ray(transform.position, transform.forward);
    //    // �浹 ������Ʈ�� ���ؼ� ��ȯ�� ����
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, moveDistance_, collisionMask,
    //        QueryTriggerInteraction.Collide))
    //    {
    //        Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
    //    }
    //    else
    //    {
    //        Gizmos.DrawRay(transform.position, transform.forward * moveDistance_);
    //    }
    //}

    void CheckCollisions(float moveDistance) 
    {
        // ���̸� ����, ���� ������ ����
        // �߻�ü�� ��ġ��, �߻�ü�� ���� ����
        Ray ray = new Ray (transform.position, transform.forward);
        // �浹 ������Ʈ�� ���ؼ� ��ȯ�� ����
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 10000, Color.blue);
        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask,
            QueryTriggerInteraction.Collide))
        // QueryTriggerInteraction Ʈ���� �ݶ��̴���� �浹�� �� ������
        {
            // ���� ���𰡿� �浹�ߴٸ� OnHitObject() ȣ��
            //Debug.Log($"�Ѿ��� �浹�ߴ���?, {moveDistance}");
            OnHitObject(hit);
        }

        //Debug.Log($"{hit}�� ���� �ϴ���?, {moveDistance}�� �����ϴ���?, {transform.rotation.eulerAngles}");
    }

    void OnHitObject(RaycastHit hit) // �浹�� ������Ʈ ������ ������ RaycastHit hit
    {
        IDamageableT damageableObject = hit.collider.GetComponent<IDamageableT> ();
        if (damageableObject != null) 
        {
            //damageableObject.TakeHit(damage, hit);
        }
        print(hit.collider.gameObject.name);
        // ������Ʈ�� �浹���� �� �߻�ü�� �ı�
        GameObject.Destroy(gameObject);

         
    }

    
}
