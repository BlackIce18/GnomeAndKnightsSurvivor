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
        ref WalletComponent _walletComponent = ref playerEntity.Get<WalletComponent>();
        _walletComponent.money = _sceneData.walletData.startMoney;
        _walletComponent.moneyIncome = _sceneData.walletData.startMoneyIncome;
        _walletComponent.killBounty = _sceneData.walletData.startCashBountyPercent;
        _sceneData.moneyText.text = _walletComponent.money.ToString();
        _sceneData.killBountyText.text = _walletComponent.killBounty.ToString();
        _sceneData.moneyIncomeText.text = _walletComponent.moneyIncome.ToString();

        _sceneData.playerStats.hp.text = _sceneData.playerData.startHp.ToString();
        _sceneData.playerStats.manaShield.text = _sceneData.playerData.startManaShield.ToString();
        _sceneData.playerStats.armor.text = _sceneData.playerData.startArmor.ToString();
        _sceneData.playerStats.hpRegen.text = _sceneData.playerData.startHpRegen.ToString();
        _sceneData.playerStats.manaShieldRegen.text = _sceneData.playerData.startManaShieldRegen.ToString();

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
