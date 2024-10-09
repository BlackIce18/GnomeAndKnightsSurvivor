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
        _distanceAttackersPool = _filter.Get1(0).meleeAttackersPool;
    }

    public void Run()
    {
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            Queue<EnemyWithPosition> distanceEnemiesToSpawn = _enemyQueueToSpawn.Get1(0).meleeEnemiesToSpawn;
            if (distanceEnemiesToSpawn.Count > 0)
            {
                while (distanceEnemiesToSpawn.Count != 0)
                {
                    EnemyWithPosition enemyWithPosition = distanceEnemiesToSpawn.Dequeue();
                    Spawn(enemyWithPosition.Position, enemyWithPosition.enemyData);
                }
            }

            _elapsedTime = 0;
        }
    }

    public void Spawn(Vector3 position, EnemyData enemyData)
    {
        EcsEntity enemyEnitity = _world.NewEntity();
        ref FollowComponent _followComponent = ref enemyEnitity.Get<FollowComponent>();
        _followComponent.target = _sceneData.player;
        ref MovableComponent _movableComponent = ref enemyEnitity.Get<MovableComponent>();
        _movableComponent.speed = enemyData.speed;
        GameObject enemy = GameObject.Instantiate(enemyData.prefab, Vector3.zero, enemyData.prefab.transform.rotation, _enemiesData.parentForEnemies);
        enemy.GetComponent<EnemyCollider>().world = _world;
        _movableComponent.transform = enemy.transform;
        ref OnTriggerEnterComponent _hitEnemyComponent = ref enemyEnitity.Get<OnTriggerEnterComponent>();
        ref DefenceComponent _defenceComponent = ref enemyEnitity.Get<DefenceComponent>();
        ref AttackComponent _attackComponent = ref enemyEnitity.Get<AttackComponent>();
        ref EnemyComponent _enemyComponent = ref enemyEnitity.Get<EnemyComponent>();
        _enemyComponent.parentPool = _distanceAttackersPool;
        _distanceAttackersPool.AddToPool(_enemyComponent);
    }
}
