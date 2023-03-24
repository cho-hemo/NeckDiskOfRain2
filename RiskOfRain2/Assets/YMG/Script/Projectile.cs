using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // LayerMask 어떤 오브젝트, 어떤 레이어가 발사체와 충돌할지 결정
    public LayerMask collisionMask;
    float speed = 10;

    public void SetSpeed(float newSpeed) 
    {
        speed = newSpeed;
    }

    void Update() // 레이캐스트 사용
    { // 충돌 감지 collision
        // 레이의 이동할 거리와 충돌에 대한 결과

        float moveDistance = speed * Time.deltaTime; // 프레임에서 이동할 거리
        CheckCollisions (moveDistance); // 새로운 매서드
        transform.Translate (Vector3.forward * moveDistance);
        Debug.DrawRay(transform.position, transform.forward, Color.red);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    //float moveDistance_ = speed * Time.deltaTime;
    //    float moveDistance_ = 10.0f;

    //    Ray ray = new Ray(transform.position, transform.forward);
    //    // 충돌 오브젝트에 대해서 반환한 정보
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

        // 레이를 정의, 시작 지점과 방향
        // 발사체의 위치와, 발사체의 정면 방향
        Ray ray = new Ray (transform.position, transform.forward);
        // 충돌 오브젝트에 대해서 반환한 정보
        RaycastHit hit;
        //Debug.DrawRay(ray.origin, ray.direction * 10000, Color.blue);
        //Debug.Log("실행");
        if (Physics.Raycast(transform.position, transform.forward, out hit, moveDistance, collisionMask
            /*QueryTriggerInteraction.Collide*/))
        // QueryTriggerInteraction 트리거 콜라이더들과 충돌할 지 안할지
        {
            // 만약 무언가와 충돌했다면 OnHitObject() 호출
            //Debug.Log($"총알이 충돌했는지?, {moveDistance}");
            OnHitObject(hit);
        }

        //Debug.Log($"{hit}가 존재 하는지?, {moveDistance}가 존재하는지?, {transform.rotation.eulerAngles}");
    }

    void OnHitObject(RaycastHit hit) // 충돌한 오브젝트 정보를 가져올 RaycastHit hit
    {
        IDamageableT damageableObject = hit.collider.GetComponent<IDamageableT> ();
        if (damageableObject != null) 
        {
            //damageableObject.TakeHit(damage, hit);
        }
        print(hit.collider.gameObject.name);
        // 오브젝트에 충돌했을 때 발사체를 파괴

        GameObject.Destroy(gameObject);
        //GameObject.Destroy(hit.transform.gameObject);
        hit.transform.GetComponent<Enemy>().TakeHit(1, hit);
         
    }

    
}
