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
        ref ActiveShopItemsComponent shop = ref shopEntity.Get<ActiveShopItemsComponent>();
        ref ShopBuyItemCommandComponent shopBuy = ref shopEntity.Get<ShopBuyItemCommandComponent>();
        ref ResetShopComponent resetShopComponent = ref shopEntity.Get<ResetShopComponent>();
        ref PurchasedItemsComponent purchasedItems = ref shopEntity.Get<PurchasedItemsComponent>();
        resetShopComponent.rollsCount = 1;
        resetShopComponent.shopEntity = shopEntity;
        resetShopComponent.isAvailable = true;

        shop.shopItems = new List<ShopItemData>();
        shopBuy.list = new List<ShopBuyItemCommand>();
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
        /*_walletComponent.money = _sceneData.walletData.startMoney;
        _walletComponent.moneyIncome = _sceneData.walletData.startMoneyIncome;
        _walletComponent.killBounty = _sceneData.walletData.startCashBountyPercent;
        _sceneData.moneyText.text = _walletComponent.money.ToString();
        _sceneData.killBountyText.text = _walletComponent.killBounty.ToString();
        _sceneData.moneyIncomeText.text = _walletComponent.moneyIncome.ToString();*/
    }
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
