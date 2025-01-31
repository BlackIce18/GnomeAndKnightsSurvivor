using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Gun", order = 1)]
public class GunData : ScriptableObject
{
    public string gunId;
    public GameObject bulletPrefab;
    public GameObject gunPrefab;
    public float attackInterval;
    public int minAttackRange;
    public int maxAttackRange;
    public AttackTypeData attackType;
    public ShopItemGunData shopItemData;
}
