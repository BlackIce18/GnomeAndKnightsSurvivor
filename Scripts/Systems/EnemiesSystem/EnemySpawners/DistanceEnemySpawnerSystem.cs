using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemySpawnerSystem : IEcsInitSystem, IEcsRunSystem, IEnemySpawner
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private EnemiesData _enemiesData;
    private EcsFilter<EnemiesPoolComponent> _filter;
    private ObjectPool<EnemyComponent> _distanceAttackersPool;

    private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;
    private int _spawnTime = 10;
    private float _elapsedTime = 0;
    
    public void Init()
    {
        _distanceAttackersPool = _filter.Get1(0).distanceAttackersPool;
    }

    public void Run()
    {
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            Queue<EnemyWithPosition> distanceEnemiesToSpawn = _enemyQueueToSpawn.Get1(0).distanceEnemiesToSpawn;
            if (distanceEnemiesToSpawn.Count > 0)
            {
                while (distanceEnemiesToSpawn.Count != 0)
                {
                    EnemyWithPosition enemyWithPosition = distanceEnemiesToSpawn.Dequeue();
                    CreateNewEnemy(enemyWithPosition.Position, enemyWithPosition.enemyData);
                }
            }

            _elapsedTime = 0;
        }
    }

    public void CreateNewEnemy(Vector3 position, EnemyData enemyData)
    {
        EcsEntity enemyEnitity = _world.NewEntity();
        ref FollowComponent _followComponent = ref enemyEnitity.Get<FollowComponent>();
        ref MovableComponent _movableComponent = ref enemyEnitity.Get<MovableComponent>();
        ref OnTriggerEnterComponent _hitEnemyComponent = ref enemyEnitity.Get<OnTriggerEnterComponent>();
        ref DefenceComponent _defenceComponent = ref enemyEnitity.Get<DefenceComponent>();
        ref AttackComponent _attackComponent = ref enemyEnitity.Get<AttackComponent>();
        ref EnemyComponent _enemyComponent = ref enemyEnitity.Get<EnemyComponent>();

        GameObject enemy = GameObject.Instantiate(enemyData.prefab, position, enemyData.prefab.transform.rotation, _enemiesData.parentForEnemies);
        _movableComponent.transform = enemy.transform;
        EnemyCollider enemyCollider = enemy.GetComponent<EnemyCollider>();
        enemyCollider.entity = enemyEnitity;
        _followComponent.target = _sceneData.player;
        _movableComponent.speed = enemyData.speed;
        _defenceComponent.hp = enemyData.defenceComponent.hp;

        _enemyComponent.parentPool = _distanceAttackersPool;
        _enemyComponent.ecsEntity = enemyEnitity;
        _enemyComponent.instance = enemy;

        _distanceAttackersPool.AddToPool(_enemyComponent);
    }

    public EnemyComponent GetFromPool(UnityEngine.Vector3 position)
    {
        EnemyComponent _enemyComponent = _distanceAttackersPool.GetFromPool();
        _enemyComponent.instance.transform.position = position;
        _enemyComponent.instance.SetActive(true);

        return _enemyComponent;
    }
}
