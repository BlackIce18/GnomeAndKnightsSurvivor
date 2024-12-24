using System;


[Serializable]
public struct DefenceComponent
{
    public int hp;
    public int maxHP;
    public int manaShield;
    public int maxManaShield;
    public int armor;
    public int hpRegen;
    public int manaShieldRegen;
    public float timeToStartHpRegenAfterTakeDamage;
    public float timeToStartManaShieldRegenAfterTakeDamage;
    public ArmorTypeEnum armorType;
}
