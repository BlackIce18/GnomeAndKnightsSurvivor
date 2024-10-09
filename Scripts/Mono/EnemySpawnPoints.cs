using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnPoint> _spawnPoints;

    public List<EnemySpawnPoint> SpawnPoints {  get { return _spawnPoints; } }
}
