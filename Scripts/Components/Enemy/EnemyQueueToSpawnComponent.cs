using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct EnemyWithPosition
{
    public Vector3 Position;
    public EnemyData enemyData;
}
public struct EnemyQueueToSpawnComponent
{
    public Queue<EnemyWithPosition> meleeEnemiesToSpawn;
    public Queue<EnemyWithPosition> distanceEnemiesToSpawn;
}
