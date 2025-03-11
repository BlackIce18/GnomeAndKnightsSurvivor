using Leopotam.Ecs;
using UnityEngine;

public class EnemySpawnerSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;

    private EcsFilter<TimerComponent> _timer;
    private int _spawnTimingIndex = 0;

    private float _spawnTime;
    private float _elapsedTime = 0;
    private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;
    private EcsFilter<TimerComponent> _timerFilter = null;

    private EcsFilter<EnemiesPoolComponent> _poolsFilter;
    private IEnemySpawner _spawner;

    private readonly Vector2[] screenDirections = new Vector2[] { Vector2.down * 25, Vector2.up * 10, Vector2.left * 15, Vector2.right * 15 };
    private EnemySpawnPoints _spawnPointsTemp;

    private float _spawnDistance = 10f;
    public void Init()
    {
        _spawnTime = _sceneData.spawnTimings.spawnTimeInterval;
        _spawner = new KnightSpawnerSystem(_world, _sceneData, _poolsFilter.Get1(0).meleeAttackersPool);
    }

    public void Run()
    {
        ref var currentTime = ref _timerFilter.Get1(0);
        if ((currentTime.minutes == 0) && (currentTime.seconds <= _sceneData.shop.ResetShopData.FreeBuyTime) && (currentTime.hours == 0))
        {
            return;
        }
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            UpdateSpawnIndexByTimer();
            Spawn();
            _elapsedTime = 0;
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

    public void Spawn()
    {
        EnemySpawnPoints enemySpawnPattern = MoveSpawnPointsPositions();

        for (int i = 0; i < enemySpawnPattern.SpawnPoints.Count; i++)
        {
            var availableEnemies = _sceneData.spawnTimings.enemies[_spawnTimingIndex].availableEnemies;
            int randomNumber = UnityEngine.Random.Range(0, availableEnemies.Count);

            EnemyData enemyData = availableEnemies[randomNumber];

            var enemyToSpawn = new EnemyWithPosition();
            enemyToSpawn.enemyData = enemyData;
            enemyToSpawn.Position = enemySpawnPattern.SpawnPoints[i].transform.position;

            _spawner.Create(enemyData, enemyToSpawn.Position);
        }
    }

    public EnemySpawnPoints GetRandomSpawnPattern()
    {
        if(_spawnPointsTemp)
        {
            GameObject.Destroy(_spawnPointsTemp.gameObject);
        }
        var patterns = _sceneData.enemySpawnPatterns.patterns;
        int randomPatternNumber = UnityEngine.Random.Range(0, patterns.Count);
        var randomScreenDirection = UnityEngine.Random.Range(0, screenDirections.Length);

        _spawnPointsTemp = GameObject.Instantiate(patterns[randomPatternNumber], Vector3.zero, Quaternion.identity);

        for(int i = 0; i < _spawnPointsTemp.SpawnPoints.Count; i++)
        {
            float x = _spawnPointsTemp.SpawnPoints[i].transform.position.x + _sceneData.player.position.x + screenDirections[randomScreenDirection].x;
            float y = _spawnPointsTemp.SpawnPoints[i].transform.position.y;
            float z = _spawnPointsTemp.SpawnPoints[i].transform.position.z + _sceneData.player.position.z + screenDirections[randomScreenDirection].y;
            _spawnPointsTemp.SpawnPoints[i].transform.position = new Vector3(x, y, z);
        }

        return _spawnPointsTemp;
    }

    private EnemySpawnPoints MoveSpawnPointsPositions() 
    {
        Vector3 bottomLeft = _sceneData.mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _sceneData.mainCamera.transform.position.y));
        Vector3 topRight = _sceneData.mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _sceneData.mainCamera.transform.position.y));
        if (_spawnPointsTemp)
        {
            GameObject.Destroy(_spawnPointsTemp.gameObject);
        }
        var patterns = _sceneData.enemySpawnPatterns.patterns;

        _spawnPointsTemp = GameObject.Instantiate(patterns[UnityEngine.Random.Range(0, patterns.Count)], Vector3.zero, Quaternion.identity);

        Bounds prefabBounds = GetPrefabBounds(_spawnPointsTemp.transform);
        Vector3 spawnPosition = GetRandomOffScreenPosition(bottomLeft, topRight, prefabBounds);
        _spawnPointsTemp.transform.localPosition = spawnPosition;
        return _spawnPointsTemp;
    }

    private Bounds GetPrefabBounds(Transform spawnPoint)
    {
        Bounds bounds = new Bounds(spawnPoint.position, Vector3.zero);
        foreach (Transform child in spawnPoint)
        {
            bounds.Encapsulate(child.position);
        }
        return bounds;
    }

    private Vector3 GetRandomOffScreenPosition(Vector3 bottomLeft, Vector3 topRight, Bounds prefabBounds)
    {
        float prefabWidth = prefabBounds.size.x;
        float prefabHeight = prefabBounds.size.y;

        int direction = Random.Range(0, 8);

        switch (direction)
        {
            case 0: // Верх
                return new Vector3(
                    Random.Range(bottomLeft.x + prefabWidth / 2, topRight.x - prefabWidth / 2),
                    0,
                    topRight.z + prefabHeight / 2 + _spawnDistance * 2
                );

            case 1: // Низ
                return new Vector3(
                    Random.Range(bottomLeft.x + prefabWidth / 2, topRight.x - prefabWidth / 2),
                    0, 
                    bottomLeft.z - prefabHeight / 2 - _spawnDistance * 2
                );


            case 2: // Лево
                return new Vector3(
                    bottomLeft.x - prefabWidth / 2 - _spawnDistance,
                    0,
                    Random.Range(bottomLeft.z + prefabHeight / 2, topRight.z - prefabHeight / 2)
                );

            case 3: // Право
                return new Vector3(
                    topRight.x + prefabWidth / 2 + _spawnDistance,
                    0,
                    Random.Range(bottomLeft.z + prefabHeight / 2, topRight.z - prefabHeight / 2)
                );

            case 4: // Верх-лево
                return new Vector3(
                    bottomLeft.x - prefabWidth / 2 - _spawnDistance,
                    0,
                    topRight.z + prefabHeight / 2 + _spawnDistance
                );

            case 5: // Верх-право
                return new Vector3(
                    topRight.x + prefabWidth / 2 + _spawnDistance,
                    0,
                    topRight.z + prefabHeight / 2 + _spawnDistance
                );

            case 6: // Низ-лево
                return new Vector3(
                    bottomLeft.x - prefabWidth / 2 - _spawnDistance,
                    0,
                    bottomLeft.z - prefabHeight / 2 - _spawnDistance
                );

            case 7: // Низ-право
                return new Vector3(
                    topRight.x + prefabWidth / 2 + _spawnDistance,
                    0,
                    bottomLeft.z - prefabHeight / 2 - _spawnDistance
                );
            default:
                return Vector3.zero;
        }
    }
}
