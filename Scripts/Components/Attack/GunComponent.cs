using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GunComponent
{
    public GameObject prefab;
    public float attackRate; // Частота спавна
    public float minAttackRange;
    public float maxAttackRange;
}
