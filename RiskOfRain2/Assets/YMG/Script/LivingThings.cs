using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingThings : MonoBehaviour, IDamageableT
{
    protected float health;
    protected bool dead;

    public void TakeHit(float damage, RaycastHit hit) 
    {
        health -= damage;
        if (health <= 0) 
        {
            //Die();
        }
    }
}
