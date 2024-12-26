using System;


[Serializable]
public struct DefenceComponent
{
    public float hp;
    public float maxHP;
    public float manaShield;
    public float maxManaShield;
    public float armor;
    public float hpRegen;
    public float manaShieldRegen;
    public float timeToStartHpRegenAfterTakeDamage;
    public float timeToStartManaShieldRegenAfterTakeDamage;
    public ArmorTypeEnum armorType;
}
