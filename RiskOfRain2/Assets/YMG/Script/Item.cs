using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // enum : ������ Ÿ�� (Ÿ�� �̸� ���� �ʿ�)
    public enum ItemType { Ammo, Coin, Grenade, Heart, Weapon};
    public ItemType type; // ������ ������ ���� ������ ���� ����
    public int value;
}
