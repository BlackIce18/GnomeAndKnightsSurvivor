using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class InitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    public void Init()
    {
        // NewEntity() используется для создания новых сущностей в контексте мира.
        EcsEntity playerEntity = _world.NewEntity();

        // Get() возвращает существующий на сущности компонент. Если компонент не существовал - он будет добавлен автоматически.
        // Следует обратить внимание на "ref" - компоненты должны обрабатываться по ссылке.
        ref MovableComponent _playerMoveComponent = ref playerEntity.Get<MovableComponent>();
        _playerMoveComponent.speed = _sceneData.playerData.playerSpeed;
        _playerMoveComponent.transform = _sceneData.player;
        ref UserInputComponent _userInputComponent = ref playerEntity.Get<UserInputComponent>();
        ref OnTriggerEnterComponent _hitComponent = ref playerEntity.Get<OnTriggerEnterComponent>();

        EcsEntity attacksEntity = _world.NewEntity();
        ref ActiveBulletsComponent _activeBullets = ref attacksEntity.Get<ActiveBulletsComponent>();
        _activeBullets.list = new List<BulletComponent>();

        EcsEntity enemyEntity = _world.NewEntity();
        ref EnemiesPoolComponent _enemiesPool = ref enemyEntity.Get<EnemiesPoolComponent>();
        _enemiesPool.meleeAttackersPool = new ObjectPool<EnemyComponent>(_sceneData.enemyPoolData.meleePoolCount);
        _enemiesPool.distanceAttackersPool = new ObjectPool<EnemyComponent>(_sceneData.enemyPoolData.distancePoolCount);
        ref EnemyQueueToSpawnComponent _enemyQueueToSpawn = ref enemyEntity.Get<EnemyQueueToSpawnComponent>();
        _enemyQueueToSpawn.meleeEnemiesToSpawn = new Queue<EnemyWithPosition>();
        _enemyQueueToSpawn.distanceEnemiesToSpawn = new Queue<EnemyWithPosition>();
        /*
        for(int i = 0; i < 2; i++)
        {
            EcsEntity enemyEnitity = _world.NewEntity();
            ref FollowComponent _followComponent = ref enemyEnitity.Get<FollowComponent>();
            _followComponent.target = _sceneData.player;
            ref MovableComponent _movableComponent = ref enemyEnitity.Get<MovableComponent>();
            _movableComponent.speed = _enemiesData.knight.speed;
            GameObject enemy = GameObject.Instantiate(_enemiesData.knight.prefab, new Vector3(Random.Range(0, 20), 0, -10), _enemiesData.knight.prefab.transform.rotation, _enemiesData.parentForEnemies);
            enemy.GetComponent<EnemyCollider>().world = _world;
            _movableComponent.transform = enemy.transform;
            ref OnTriggerEnterComponent _hitEnemyComponent = ref enemyEnitity.Get<OnTriggerEnterComponent>();
            ref DefenceComponent _defenceComponent = ref enemyEnitity.Get<DefenceComponent>();
        }*/

    }
}
