using System;
public enum ArmorType
{
    NoArmor,
    Light,
    Normal,
    Heavy,
    Siege,
    Heroic
}
[Serializable]
public struct DefenceComponent
{
    public int hp;
    public int armor;
    public int manaShield;
    public float hpRegen;
    public float manaShieldRegen;
    public ArmorType armorType;
}
