using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LvlAndXp", order = 1)]
public class LvlAndXpData : ScriptableObject
{
    public List<int> xpToReachLevel = new List<int>();
}
