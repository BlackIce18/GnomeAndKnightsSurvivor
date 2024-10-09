using Leopotam.Ecs;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawnerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private EnemiesData _enemiesData;

    private EcsFilter<TimerComponent> _timer;
    private int _spawnTimingIndex = 0;

    private int _spawnTime = 10;
    private float _elapsedTime = 0;
    private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;

    public void Init()
    {
    }

    public void Spawn()
    {
        UpdateSpawnIndexByTimer();
        EnemySpawnPoints enemySpawnPattern = GetRandomSpawnPattern();

        for(int i = 0; i < enemySpawnPattern.SpawnPoints.Count; i++)
        {
            var availableEnemies = _sceneData.spawnTimings.enemies[_spawnTimingIndex].availableEnemies;
            int randomNumber = UnityEngine.Random.Range(0, availableEnemies.Count);

            Vector3 spawnPosition = enemySpawnPattern.SpawnPoints[i].transform.localPosition;
            EnemyData enemyData = availableEnemies[randomNumber];

            var enemyToSpawn = new EnemyWithPosition();
            enemyToSpawn.enemyData = enemyData;
            enemyToSpawn.Position = spawnPosition;

            switch (enemyData.attackComponent.AttackRange)
            {
                case AttackRange.Melee:
                _enemyQueueToSpawn.Get1(0).meleeEnemiesToSpawn.Enqueue(enemyToSpawn);
                break;
                case AttackRange.Distance:
                _enemyQueueToSpawn.Get1(0).distanceEnemiesToSpawn.Enqueue(enemyToSpawn);
                break;
            }
        }
    }

    public void Run()
    {
        if((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            Spawn();
            _elapsedTime = 0;
        }
    }

    public void UpdateSpawnIndexByTimer()
    {
        ref TimerComponent _currentTime = ref _timer.Get1(0);
        for (int i = 0; i < _sceneData.spawnTimings.enemies.Count; i++)
        {
            TimerComponent _enemySpawnTiming = _sceneData.spawnTimings.enemies[i].time;
            if (_currentTime.minutes == _enemySpawnTiming.minutes)
            {
                if (_currentTime.seconds >= _enemySpawnTiming.seconds)
                {
                    _spawnTimingIndex = i;
                }
            }
        }
    }

    public EnemySpawnPoints GetRandomSpawnPattern()
    {
        int randomPatternNumber = UnityEngine.Random.Range(0, _sceneData.enemySpawnPatterns.patterns.Count);
        return _sceneData.enemySpawnPatterns.patterns[randomPatternNumber];
    }
}
