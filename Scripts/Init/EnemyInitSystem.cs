using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    public void Init()
    {
        EcsEntity enemyEntity = _world.NewEntity();
        ref EnemiesPoolComponent enemiesPool = ref enemyEntity.Get<EnemiesPoolComponent>();
        enemiesPool.meleeAttackersPool = new ObjectPool<EnemyComponent>(_sceneData.enemyPoolData.meleePoolCount);
        enemiesPool.distanceAttackersPool = new ObjectPool<EnemyComponent>(_sceneData.enemyPoolData.distancePoolCount);
        ref EnemyQueueToSpawnComponent enemyQueueToSpawn = ref enemyEntity.Get<EnemyQueueToSpawnComponent>();
        enemyQueueToSpawn.meleeEnemiesToSpawn = new Queue<EnemyWithPosition>();
        enemyQueueToSpawn.distanceEnemiesToSpawn = new Queue<EnemyWithPosition>();
    }
}
