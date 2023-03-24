using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour, IDamageable // 몬스터 공통
{
    // 필드
    public float maxHp = default; // 최대 체력
    public float hp = default; // 현재 체력
    public bool dead = default; // 생존 확인
    public event Action OnDeath; // 사망 시 발동할 이벤트

    //protected Transform enemyObj;


    protected virtual void OnEnable() 
    {
        // private == 내부에서만 접근
        // protected == 자식 클래스는 접근 가능

        // 사망하지 않은 상태로 시작
        dead = false;
        // 체력을 최대 체력으로 초기화
        hp = maxHp;
        
    }

    // 데미지를 입는 기능
    public virtual void OnDamage(float damage) 
    {
        // 데미지 만큼 체력 감소
        maxHp -= damage;
        Debug.Log(transform.name + "take" + damage + "damage");

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리
        if (maxHp <= 0 && !dead) 
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        // OnDeath 이벤트에 등록된 메서드가 있다면 실행
        if (OnDeath != null) 
        {
            OnDeath();
        }

        dead = true;
    }

    // 회복
    public virtual void Restore(float maxHp) 
    {
        if (dead) // 죽으면 회복 안됨
        {
            return;
        }

        hp += maxHp;
    }

    public virtual void CloseCombat() 
    {
        Debug.Log("근접 공격");
    }

    public virtual void LongCombat() 
    {
        Debug.Log("원거리 공격");
    }

    //protected enum MonsterState
    //{
    //    Attack,
    //    Move,
    //    OnDamaged
    //}

    //protected void DoAction(MonsterState monsterMode)
    //{

    //    switch (monsterMode)
    //    {
    //        case MonsterState.Attack:
    //            Debug.Log("근접 공격");
    //            break;
    //        case MonsterState.Move:
                
    //            break;
    //        case MonsterState.OnDamaged:
                
    //            break;
    //    }
    //}
}


