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
    public AttackType AttackType;
    public AttackRange AttackRange;
    public int damage;
    public int range;
    public float attackRate;
    public float critChance;
    public float critDamagePercantage;
}
