using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RangedAttackComponent
{
    public float range;
    public GameObject projectilePrefab; // ������ �������
    public float attackCooldown;
    public int damage;
    public float projectileSpeed;
}
