using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/SpawnPatternConfig", order = 1)]
public class EnemySpawnPatterns : ScriptableObject
{
    public List<EnemySpawnPoints> patterns;
}
