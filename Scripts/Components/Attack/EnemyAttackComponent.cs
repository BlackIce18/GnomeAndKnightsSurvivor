using System;
using System.Collections.Generic;

public enum AttackRange
{
    Melee,
    Distance
}
[Serializable]
public struct EnemyAttackComponent
{
    public AttackType attackType;
    public AttackRange attackRange;
    public float damage;
    public float range;
    public float viewRange;
    public float attackRate;
    public float critChance;
    public float critDamageMultiplier;
}
