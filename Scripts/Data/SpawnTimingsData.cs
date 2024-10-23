using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct PossibilityToSpawnEnemy
{
    public List<EnemyData> availableEnemies;
    public TimerComponent time;
}
[CreateAssetMenu(menuName = "ScriptableObjects/SpawnTimingsConfig", order = 1)]
public class SpawnTimingsData : ScriptableObject
{
    public List<PossibilityToSpawnEnemy> enemies;
    public float spawnTimeInterval;
}
