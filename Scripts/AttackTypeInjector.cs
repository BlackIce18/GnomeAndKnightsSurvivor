using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypeInjector
{
    public GunsInitSystem GunsInitSystem;
    public void Inject(IAttackType attackType, AttackTypeData attackTypeData)
    {
        /*IAttackType attack;

        switch (attackType)
        {
            case NormalAttack:
                attack = new NormalAttack(attackTypeData);
                break;
            case PiercingAttack: 
                attack = new PiercingAttack(attackTypeData);
                break;
            default:
                attack = new NormalAttack(attackTypeData);
                break;
        }

        GunsInitSystem.Inject(attack);*/
    }
}
