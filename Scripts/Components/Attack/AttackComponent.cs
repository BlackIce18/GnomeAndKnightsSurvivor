using System;

public enum AttackType
{
    Normal,
    Piercing,
    Magic,
    Siege,
    Heroic
}
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
