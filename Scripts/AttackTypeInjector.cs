using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeInjector
{
    public GunsSpawnSystem GunsInitSystem;
    public AttackTypeInjector(GunsSpawnSystem gunsInitSystem) 
    {
        GunsInitSystem = gunsInitSystem;
    }
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
            default:
                attackType = new ClearAttack(attackTypeData);
                break;
        }

        return attackType;
    }
}
