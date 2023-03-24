using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // enum : 열거형 타입 (타입 이름 지정 필요)
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon};
    public ItemType type; // 아이템 종류와 값을 저장할 변수 선언
    public int value;
}
