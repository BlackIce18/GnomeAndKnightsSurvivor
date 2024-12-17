using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemySpawnerSystem : IEnemySpawner
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private EnemiesData _enemiesData;
    private ObjectPool<EnemyComponent> _meleeAttackersPool;

    //private EcsFilter<EnemyQueueToSpawnComponent> _enemyQueueToSpawn;

    public MeleeEnemySpawnerSystem(EcsWorld world, SceneData sceneData, EnemiesData enemiesData, ObjectPool<EnemyComponent> pool)
    {
        _world = world;
        _sceneData = sceneData;
        _enemiesData = enemiesData;
        _meleeAttackersPool = pool;
    }

    /*public void Spawn()
    {
        Queue<EnemyWithPosition> meleeEnemiesToSpawn = _enemyQueueToSpawn.Get1(0).meleeEnemiesToSpawn;
        if (meleeEnemiesToSpawn.Count > 0)
        {
            while (meleeEnemiesToSpawn.Count != 0)
            {
                EnemyWithPosition enemyWithPosition = meleeEnemiesToSpawn.Dequeue();
                Debug.Log(_meleeAttackersPool.PoolSize);
                if (_meleeAttackersPool.PoolSize == 0)
                {
                    CreateNewEnemy(enemyWithPosition.Position, enemyWithPosition.enemyData);
                }
                else
                {
                    GetFromPool(enemyWithPosition.Position);
                }
            }
        }
    }*/

    public void CreateNewEnemy(Vector3 position, EnemyData enemyData)
    {
        EnemyComponent _enemyComponentFromPool = _meleeAttackersPool.GetFromPool();
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
        ref DefenceComponent _defenceComponent = ref enemyEnitity.Get<DefenceComponent>();
        ref EnemyAttackComponent _attackComponent = ref enemyEnitity.Get<EnemyAttackComponent>();
        ref EnemyComponent _enemyComponent = ref enemyEnitity.Get<EnemyComponent>();

        GameObject enemy = GameObject.Instantiate(enemyData.prefab, position, enemyData.prefab.transform.rotation, _enemiesData.parentForEnemies);
        _movableComponent.transform = enemy.transform;
        EnemyCollider enemyCollider = enemy.GetComponent<EnemyCollider>();
        enemyCollider.entity = enemyEnitity;
        _followComponent.target = _sceneData.player;
        _movableComponent.speed = enemyData.speed + Random.Range(-0.25f, 0.25f);
        _defenceComponent.hp = enemyData.defenceComponent.hp;

        _enemyComponent.parentPool = _meleeAttackersPool;
        _enemyComponent.ecsEntity = enemyEnitity;
        _enemyComponent.instance = enemy;
        _enemyComponent.enemyData = enemyData;

        _meleeAttackersPool.AddToPool(_enemyComponent);
    }
}
