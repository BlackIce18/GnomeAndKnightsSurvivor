using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/PoolInit", order = 1)]
public class EnemyPoolData : ScriptableObject
{
    public int meleePoolCount;
    public int magePoolCount;
    public int distancePoolCount;
}
