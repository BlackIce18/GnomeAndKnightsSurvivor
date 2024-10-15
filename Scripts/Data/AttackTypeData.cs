using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackTypeAndDefenceDecrease
{
    public ArmorType armorType;
    public int defencePercent;
}

[CreateAssetMenu(menuName = "ScriptableObjects/AttackType", order = 1)]
public class AttackTypeData : ScriptableObject
{
    public List<AttackTypeAndDefenceDecrease> attacks;
}
