using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DefenceDecreases
{
    public ArmorType armorType;
    public int defencePercent;
}



[CreateAssetMenu(menuName = "ScriptableObjects/AttackType", order = 1)]
public class AttackTypeData : ScriptableObject
{
    public List<DefenceDecreases> attacks;
    public AttackTypeEnum attackType;
    /*
            { ArmorType.NoArmor, 100},
            { ArmorType.Light, 90},
            { ArmorType.Normal, 100},
            { ArmorType.Heavy, 75},
            { ArmorType.Siege, 50},
            { ArmorType.Heroic, 50}
     */
}
