using System;
using System.Collections.Generic;

public enum AttackRange
{
    Melee,
    Distance
}
[Serializable]
public struct AttackComponent
{
    public AttackType AttackType;
    public AttackRange AttackRange;
    public int damage;
}
