using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class InitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    public void Init()
    {
        // NewEntity() ������������ ��� �������� ����� ��������� � ��������� ����.
        EcsEntity playerEntity = _world.NewEntity();

        // Get() ���������� ������������ �� �������� ���������. ���� ��������� �� ����������� - �� ����� �������� �������������.
        // ������� �������� �������� �� "ref" - ���������� ������ �������������� �� ������.
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

    }
}
