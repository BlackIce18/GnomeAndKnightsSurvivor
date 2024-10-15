using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public abstract class AttackType : IAttackType
{
    protected abstract AttackTypeData AttackTypeData { get; set; }
    public abstract int CalculateDamage(DefenceComponent defenceComponent, float damage);
    /*
        readonly Dictionary<ArmorType, int> damageTable = new Dictionary<ArmorType, int>()
        {
            { ArmorType.NoArmor, 100},
            { ArmorType.Light, 90},
            { ArmorType.Normal, 100},
            { ArmorType.Heavy, 75},
            { ArmorType.Siege, 50},
            { ArmorType.Heroic, 50}
        };
    */
    /*public int Attack(DefenceComponent defenceComponent, float damage)
    {
        int defencePercent = 1;

        foreach (var i in AttackTypeData.attacks)
        {
            if (i.armorType == defenceComponent.armorType)
            {
                defencePercent = i.defencePercent;
            }
        }
        return Convert.ToInt32(damage * defencePercent / 100);
    }*/
}
public class NormalAttack : AttackType
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
public class PiercingAttack : AttackType
{
    /*
    private readonly Dictionary<ArmorType, int> damageTable = new Dictionary<ArmorType, int>()
    {
        { ArmorType.NoArmor, 100},
        { ArmorType.Light, 120},
        { ArmorType.Normal, 90},
        { ArmorType.Heavy, 75},
        { ArmorType.Siege, 30},
        { ArmorType.Heroic, 50} //465
    };
    */
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