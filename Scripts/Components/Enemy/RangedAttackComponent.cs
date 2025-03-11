using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RangedAttackComponent
{
    public float range;
    public GameObject projectilePrefab; // Префаб снаряда
    public float attackCooldown;
    public int damage;
    public float projectileSpeed;
}
