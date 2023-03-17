using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{ // 무기를 장비하는 등의 관리

    public Transform weaponHold;
    public Gun startingGun;
    Gun equippedGun; // 장착중인 총 

    void Start() 
    {
        if (startingGun != null) 
        {
            EquipGun(startingGun);
        }
    }

    public void EquipGun(Gun gunToEquip) // 장착할 총 
    {
        if (equippedGun != null) 
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip,
            weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot() 
    {
        if (equippedGun != null) 
        {
            equippedGun.Shoot();
        }
    }
}
