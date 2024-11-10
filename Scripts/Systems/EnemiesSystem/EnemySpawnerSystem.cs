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

    private float _spawnTime;
    private float _elapsedTime = 0;
    private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;

    private EcsFilter<EnemiesPoolComponent> _poolsFilter;
    private IEnemySpawner _distanceEnemySpawner;
    private IEnemySpawner _meleeEnemySpawner;

    private readonly Vector2[] screenDirections = new Vector2[] { Vector2.down * 25, Vector2.up * 10, Vector2.left * 15, Vector2.right * 15 };
    public void Init()
    {
        _spawnTime = _sceneData.spawnTimings.spawnTimeInterval;
        _meleeEnemySpawner = new MeleeEnemySpawnerSystem(_world, _sceneData, _enemiesData, _poolsFilter.Get1(0).meleeAttackersPool);
        _distanceEnemySpawner = new DistanceEnemySpawnerSystem(_world, _sceneData, _enemiesData, _poolsFilter.Get1(0).distanceAttackersPool);
    }

    public void Run()
    {
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            UpdateSpawnIndexByTimer();
            Spawn();
            _elapsedTime = 0;
        }
    }

    public void Spawn()
    {
        EnemySpawnPoints enemySpawnPattern = GetRandomSpawnPattern();

        for (int i = 0; i < enemySpawnPattern.SpawnPoints.Count; i++)
        {
            var availableEnemies = _sceneData.spawnTimings.enemies[_spawnTimingIndex].availableEnemies;
            int randomNumber = UnityEngine.Random.Range(0, availableEnemies.Count);

            EnemyData enemyData = availableEnemies[randomNumber];

            var enemyToSpawn = new EnemyWithPosition();
            enemyToSpawn.enemyData = enemyData;
            enemyToSpawn.Position = enemySpawnPattern.SpawnPoints[i].transform.position;

            IEnemySpawner enemySpawner;
            switch (enemyData.attackComponent.AttackRange)
            {
                case AttackRange.Melee:
                    enemySpawner = _meleeEnemySpawner;
                    break;
                case AttackRange.Distance:
                    enemySpawner = _distanceEnemySpawner;
                    break;
                default:
                    enemySpawner = _meleeEnemySpawner;
                    break;
            }
            enemySpawner.CreateNewEnemy(enemyToSpawn.Position, enemyData);
        }
    }

    // Изменяется доступный пул мобов для спавна
    // К примеру, на 1-ой минуте только мили мобы
    // на 2-ой добавляются мобы с дистанционнной атакой
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
        EnemySpawnPoints enemySpawnPoints = _sceneData.enemySpawnPatterns.patterns[randomPatternNumber];

        var randomDirection = Random.Range(0, screenDirections.Length-1);
        for(int i = 0; i < enemySpawnPoints.SpawnPoints.Count; i++)
        {
            enemySpawnPoints.SpawnPoints[i].transform.position = new Vector3(enemySpawnPoints.SpawnPoints[i].transform.position.x + _sceneData.player.position.x + screenDirections[randomDirection].x, enemySpawnPoints.SpawnPoints[i].transform.position.y, enemySpawnPoints.SpawnPoints[i].transform.position.z + _sceneData.player.position.z + screenDirections[randomDirection].y);
        }

        return enemySpawnPoints;
    }
}
