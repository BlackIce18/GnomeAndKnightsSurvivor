using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DefenceDecreases
{
    public ArmorTypeEnum armorType;
    public int defencePercent;

    public DefenceDecreases(ArmorTypeEnum armorType, int defencePercent)
    {
        this.armorType = armorType;
        this.defencePercent = defencePercent;
    }
}



[CreateAssetMenu(menuName = "ScriptableObjects/AttackType", order = 1)]
public class AttackTypeData : ScriptableObject
{
    public List<DefenceDecreases> attacks = new List<DefenceDecreases> 
                                           {
                                            new DefenceDecreases(ArmorTypeEnum.NoArmor, 100), 
                                            new DefenceDecreases(ArmorTypeEnum.Light, 100),
                                            new DefenceDecreases(ArmorTypeEnum.Normal, 100),
                                            new DefenceDecreases(ArmorTypeEnum.Heavy, 100),
                                            new DefenceDecreases(ArmorTypeEnum.Siege, 100),
                                            new DefenceDecreases(ArmorTypeEnum.Heroic, 100)
                                            };
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
