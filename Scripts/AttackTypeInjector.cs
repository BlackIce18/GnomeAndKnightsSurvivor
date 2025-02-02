using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeInjector
{
    public IAttackType Inject(AttackTypeData attackTypeData)
    {
        IAttackType attackType;

        switch (attackTypeData.attackType)
        {
            case AttackTypeEnum.Normal:
                attackType = new NormalAttack(attackTypeData);
                break;
            case AttackTypeEnum.Piercing:
                attackType = new PiercingAttack(attackTypeData);
                break;
            case AttackTypeEnum.Magic:
                attackType = new MagicAttack(attackTypeData);
                break;
            default:
                attackType = new ClearAttack(attackTypeData);
                break;
        }

        return attackType;
    }
}
