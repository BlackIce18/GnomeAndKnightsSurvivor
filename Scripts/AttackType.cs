using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

[Serializable]
public enum AttackTypeEnum 
{
    Normal,
    Piercing
}

public abstract class AttackType : IAttackType
{
    protected abstract AttackTypeData AttackTypeData { get; set; }
    public abstract int CalculateDamage(DefenceComponent defenceComponent, float damage);
}
public class NormalAttack : AttackType, IAttackType
{
    private Dictionary<ArmorType, int> _damageTable = new Dictionary<ArmorType, int>();
    private AttackTypeData _attackTypeData;
    protected override AttackTypeData AttackTypeData { get => _attackTypeData; set => _attackTypeData = value; }
    public NormalAttack(AttackTypeData attackTypeData)
    {
        AttackTypeData = attackTypeData;

        foreach(var i in AttackTypeData.attacks)
        {
            _damageTable.Add(i.armorType, i.defencePercent);
        }
    }

    public override int CalculateDamage(DefenceComponent defenceComponent, float damage)
    {
        return Convert.ToInt32(damage * _damageTable[defenceComponent.armorType] / 100);
    }
}
public class PiercingAttack : AttackType, IAttackType
{
    private Dictionary<ArmorType, int> _damageTable = new Dictionary<ArmorType, int>();
    private AttackTypeData _attackTypeData;
    protected override AttackTypeData AttackTypeData { get => _attackTypeData; set => _attackTypeData = value; }
    public PiercingAttack(AttackTypeData attackTypeData)
    {
        AttackTypeData = attackTypeData;

        foreach (var i in AttackTypeData.attacks)
        {
            _damageTable.Add(i.armorType, i.defencePercent);
        }
    }

    public override int CalculateDamage(DefenceComponent defenceComponent, float damage)
    {
        return Convert.ToInt32(damage * _damageTable[defenceComponent.armorType] / 100);
    }
}