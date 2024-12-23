using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class InitSystem : IEcsPreInitSystem, IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    public void PreInit()
    {
        var timerEntity = _world.NewEntity();
        ref TimerComponent timer = ref timerEntity.Get<TimerComponent>();

        var shopEntity = _world.NewEntity();
        ref ActiveShopItemsComponent activeShopItems = ref shopEntity.Get<ActiveShopItemsComponent>();
        ref ShopBuyItemCommandComponent shopBuy = ref shopEntity.Get<ShopBuyItemCommandComponent>();
        ref ResetShopComponent resetShopComponent = ref shopEntity.Get<ResetShopComponent>();
        ref PurchasedItemsComponent purchasedItems = ref shopEntity.Get<PurchasedItemsComponent>();
        resetShopComponent.rollsCount = 1;
        resetShopComponent.shopEntity = shopEntity;
        resetShopComponent.isAvailable = true;

        activeShopItems.shopItems = new List<ShopItemGunData>();
        shopBuy.list = new List<ICommand>();
        shopBuy.isAvailable = true;
        shopBuy.shopEntity = shopEntity;
        purchasedItems.items = new List<ShopItemData>();

        var walletEntity = _world.NewEntity();
        ref WalletComponent _walletComponent = ref walletEntity.Get<WalletComponent>();
        ref WalletUpdateComponent _walletUpdate = ref walletEntity.Get<WalletUpdateComponent>();
        _walletUpdate.money = _sceneData.walletData.startMoney;
        _walletUpdate.moneyIncome = _sceneData.walletData.startMoneyIncome;
        _walletUpdate.killBounty = _sceneData.walletData.startKillBountyPercent;
        _sceneData.shop.CurrentKillBounty = _sceneData.walletData.startKillBountyPercent;
    }
    public void Init()
    {
        EcsEntity playerEntity = _world.NewEntity();
        ref MovableComponent _playerMoveComponent = ref playerEntity.Get<MovableComponent>();
        _playerMoveComponent.speed = _sceneData.playerData.playerSpeed;
        _playerMoveComponent.transform = _sceneData.player;
        ref UserInputComponent _userInputComponent = ref playerEntity.Get<UserInputComponent>();
        ref OnTriggerEnterComponent _hitComponent = ref playerEntity.Get<OnTriggerEnterComponent>();


        ref PlayerHealthComponent playerHealthComponent = ref playerEntity.Get<PlayerHealthComponent>();
        playerHealthComponent.currentHealthPoints = _sceneData.playerData.startHp;
        ref PlayerMaxHealthUpdateEventComponent playerMaxHealthUpdateEventComponent = ref playerEntity.Get<PlayerMaxHealthUpdateEventComponent>();
        playerMaxHealthUpdateEventComponent.newMaxHealthPoints = _sceneData.playerData.startHp;
        ref PlayerHealthUpdateEventComponent playerHealthUpdateEventComponent = ref playerEntity.Get<PlayerHealthUpdateEventComponent>();
        playerHealthUpdateEventComponent.newHealthPoints = _sceneData.playerData.startHp;

        ref PlayerManaShieldComponent playerManaShieldComponent = ref playerEntity.Get<PlayerManaShieldComponent>();
        playerManaShieldComponent.currentManaShield = _sceneData.playerData.startManaShield;
        ref PlayerMaxManaShieldUpdateEventComponent playerMaxManaShieldUpdateEventComponent = ref playerEntity.Get<PlayerMaxManaShieldUpdateEventComponent>();
        playerMaxManaShieldUpdateEventComponent.newMaxManaShield = _sceneData.playerData.startManaShield;
        ref PlayerManaShieldUpdateEventComponent playerManaShieldUpdateEventComponent = ref playerEntity.Get<PlayerManaShieldUpdateEventComponent>();
        playerManaShieldUpdateEventComponent.newManaShield = _sceneData.playerData.startManaShield;

        ref CurrentPlayerCharacteristicsComponent currentPlayerStats = ref playerEntity.Get<CurrentPlayerCharacteristicsComponent>();
        currentPlayerStats.healthPoint = playerHealthComponent.currentHealthPoints;
        currentPlayerStats.maxHealthPoint = playerHealthComponent.maxHealthPoints;
        currentPlayerStats.manaShield = playerManaShieldComponent.currentManaShield;
        currentPlayerStats.maxManaShield = playerManaShieldComponent.maxManaShield;
        currentPlayerStats.playerSpeed = _playerMoveComponent.speed;
        currentPlayerStats.armor = _sceneData.playerData.startArmor;
        currentPlayerStats.healthPointRegen = _sceneData.playerData.startHpRegen;
        currentPlayerStats.manaShieldRegen = _sceneData.playerData.startManaShieldRegen;
        currentPlayerStats.timeToStartHpRegenAfterTakeDamage = _sceneData.playerData.timeToStartHpRegenAfterTakeDamage;
        currentPlayerStats.timeToStartManaShieldRegenAfterTakeDamage = _sceneData.playerData.timeToStartManaShieldRegenAfterTakeDamage;

    //_sceneData.playerStats.hp.text = _sceneData.playerData.startHp.ToString();
    //_sceneData.playerStats.manaShield.text = _sceneData.playerData.startManaShield.ToString();
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

        EcsEntity damageTextEntity = _world.NewEntity();
        ref ActiveDamageText activeDamageText = ref damageTextEntity.Get<ActiveDamageText>();
        activeDamageText.damageTexts = new List<DamageTextRemoveTimeStruct>();
    }
}
