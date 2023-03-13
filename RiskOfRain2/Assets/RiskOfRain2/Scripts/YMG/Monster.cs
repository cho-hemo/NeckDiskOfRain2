using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamageable // ���� ����
{
    // �ʵ�
    public float closerDamage = default; // ���� ���ݷ�
    public float longerDamage = default; // ���Ÿ� ���ݷ�
    public float maxHp = default; // �ִ� ü��
    public float hp = default; // ���� ü��
    public bool dead = default; // ���� Ȯ��
    public event Action OnDeath; // ��� �� �ߵ��� �̺�Ʈ

    
    

    protected virtual void OnEnable() 
    {
        // private == ���ο����� ����
        // protected == �ڽ� Ŭ������ ���� ����

        // ������� ���� ���·� ����
        dead = false;
        // ü���� �ִ� ü������ �ʱ�ȭ
        hp = maxHp;
        
    }

    // �������� �Դ� ���
    public virtual void OnDamage(float damage) 
    {
        // ������ ��ŭ ü�� ����
        maxHp -= damage;
        Debug.Log(transform.name + "take" + damage + "damage");

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó��
        if (maxHp <= 0 && !dead) 
        {
            Die();
        }
    }

    public virtual void Die() 
    {
        // OnDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        if (OnDeath != null) 
        {
            OnDeath();
        }

        dead = true;
    }

    // ȸ��
    public virtual void Restore(float maxHp) 
    {
        if (dead) // ������ ȸ�� �ȵ�
        {
            return;
        }

        hp += maxHp;
    }

    public virtual void CloseCombat() 
    {
        Debug.Log("���� ����");
    }

    public virtual void LongCombat() 
    {
        Debug.Log("���Ÿ� ����");
    }

}


