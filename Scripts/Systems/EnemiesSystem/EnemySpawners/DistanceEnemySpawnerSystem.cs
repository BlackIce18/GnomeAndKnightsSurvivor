using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemySpawnerSystem : IEnemySpawner
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private EcsFilter<EnemiesPoolComponent> _filter;
    private ObjectPool<EnemyComponent> _distanceAttackersPool;

    private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;

    public DistanceEnemySpawnerSystem(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool)
    {
        _world = world;
        _sceneData = sceneData;
        _distanceAttackersPool = pool;
    }

    public void Create(EnemyData enemyData, Vector3 position)
    {
        EnemyComponent _enemyComponentFromPool = _distanceAttackersPool.GetFromPool();
        if(_enemyComponentFromPool.instance != null)
        {
            _enemyComponentFromPool.instance.transform.position = position;
            _enemyComponentFromPool.instance.SetActive(true);
            _enemyComponentFromPool.ecsEntity.Get<DefenceComponent>().hp = enemyData.defenceComponent.hp;
            return;
        }

        EcsEntity enemyEnitity = _world.NewEntity();
        ref FollowComponent _followComponent = ref enemyEnitity.Get<FollowComponent>();
        ref MovableComponent _movableComponent = ref enemyEnitity.Get<MovableComponent>();
        ref OnTriggerEnterComponent _hitEnemyComponent = ref enemyEnitity.Get<OnTriggerEnterComponent>();
        ref DefenceComponent _defenceComponent = ref enemyEnitity.Get<DefenceComponent>();
        ref EnemyAttackComponent _attackComponent = ref enemyEnitity.Get<EnemyAttackComponent>();
        ref EnemyComponent _enemyComponent = ref enemyEnitity.Get<EnemyComponent>();

        GameObject enemy = GameObject.Instantiate(enemyData.prefab, position, enemyData.prefab.transform.rotation, _sceneData.enemyParentObject);
        enemy.SetActive(false);
        _movableComponent.transform = enemy.transform;
        EnemyCollider enemyCollider = enemy.GetComponent<EnemyCollider>();
        enemyCollider.entity = enemyEnitity;
        _followComponent.target = _sceneData.player;
        _movableComponent.speed = enemyData.speed + Random.Range(-0.25f, 0.25f);
        _defenceComponent.hp = enemyData.defenceComponent.hp;

        _enemyComponent.parentPool = _distanceAttackersPool;
        _enemyComponent.ecsEntity = enemyEnitity;
        _enemyComponent.instance = enemy;
        _enemyComponent.enemyData = enemyData;

        _distanceAttackersPool.AddToPool(_enemyComponent);
    }
}
