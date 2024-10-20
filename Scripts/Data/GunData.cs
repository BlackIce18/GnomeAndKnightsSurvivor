using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Gun", order = 1)]
public class GunData : ScriptableObject
{
    public GameObject prefab;
    public float attackRate;
    public int minAttackRange;
    public int maxAttackRange;
    public AttackTypeData attackType;
}
