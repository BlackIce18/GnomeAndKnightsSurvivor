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
        ref ShopBuyItemCommandComponent shopBuy = ref shopEntity.Get<ShopBuyItemCommandComponent>();
        ref ResetShopComponent resetShopComponent = ref shopEntity.Get<ResetShopComponent>();
        ref PurchasedItemsComponent purchasedItems = ref shopEntity.Get<PurchasedItemsComponent>();
        ref ShopBuyCommandsComponent shopBuyCommands = ref shopEntity.Get<ShopBuyCommandsComponent>();
        shopBuyCommands.buyCommands = new List<ShopBuyCommand>();

        resetShopComponent.rollsCount = 1;
        resetShopComponent.shopEntity = shopEntity;
        resetShopComponent.isAvailable = true;

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


        ResetShopCommand resetCommand = new ResetShopCommand(_sceneData, shopEntity, walletEntity, timerEntity, purchasedItems);
        _sceneData.shop.AddOnClick(_sceneData.shop.ResetButton, resetCommand);
        resetShopComponent.resetShopCommand = resetCommand;
    }
    public void Init()
    {
        EcsEntity playerEntity = _world.NewEntity();
        ref MovableComponent playerMoveComponent = ref playerEntity.Get<MovableComponent>();
        playerMoveComponent.speed = _sceneData.playerData.startSpeed;
        playerMoveComponent.transform = _sceneData.player;
        ref UserInputComponent userInputComponent = ref playerEntity.Get<UserInputComponent>();
        ref OnTriggerEnterComponent hitComponent = ref playerEntity.Get<OnTriggerEnterComponent>();

        ref DefenceComponent playerDefenceComponent = ref playerEntity.Get<DefenceComponent>();
        ref HPRegenComponent hpRegenComponent = ref playerEntity.Get<HPRegenComponent>();
        ref ManaShieldRegenComponent manaShieldRegenComponent = ref playerEntity.Get<ManaShieldRegenComponent>();

        playerEntity.Get<ManaShieldRegenComponent>();
        playerEntity.Get<PlayerHPManaShieldUpdateEventComponent>();

        ref MaxHpUpdateEventComponent playerMaxHealthUpdateEventComponent = ref playerEntity.Get<MaxHpUpdateEventComponent>();
        playerMaxHealthUpdateEventComponent.newMaxHealthPoints = _sceneData.playerData.startHp;
        ref HpUpdateEventComponent playerHealthUpdateEventComponent = ref playerEntity.Get<HpUpdateEventComponent>();
        playerHealthUpdateEventComponent.newHealthPoints = _sceneData.playerData.startHp;

        ref MaxManaShieldUpdateEventComponent playerMaxManaShieldUpdateEventComponent = ref playerEntity.Get<MaxManaShieldUpdateEventComponent>();
        playerMaxManaShieldUpdateEventComponent.newMaxManaShield = _sceneData.playerData.startManaShield;
        ref ManaShieldUpdateEventComponent playerManaShieldUpdateEventComponent = ref playerEntity.Get<ManaShieldUpdateEventComponent>();
        playerManaShieldUpdateEventComponent.newManaShield = _sceneData.playerData.startManaShield;

        ref DefenceComponent currentPlayerStats = ref playerEntity.Get<DefenceComponent>();
        currentPlayerStats.hp = _sceneData.playerData.startHp;
        currentPlayerStats.maxHP = _sceneData.playerData.startHp;
        currentPlayerStats.manaShield = _sceneData.playerData.startManaShield;
        currentPlayerStats.maxManaShield = _sceneData.playerData.startManaShield;
        currentPlayerStats.armor = _sceneData.playerData.startArmor;
        currentPlayerStats.hpRegen = _sceneData.playerData.startHpRegen;
        currentPlayerStats.manaShieldRegen = _sceneData.playerData.startManaShieldRegen;
        currentPlayerStats.timeToStartHpRegenAfterTakeDamage = _sceneData.playerData.timeToStartHpRegenAfterTakeDamage;
        currentPlayerStats.timeToStartManaShieldRegenAfterTakeDamage = _sceneData.playerData.timeToStartManaShieldRegenAfterTakeDamage;
        
        //currentPlayerStats.speed = _playerMoveComponent.speed;

        _sceneData.playerStats.armor.text = _sceneData.playerData.startArmor.ToString();
        _sceneData.playerStats.hpRegen.text = _sceneData.playerData.startHpRegen.ToString();
        _sceneData.playerStats.manaShieldRegen.text = _sceneData.playerData.startManaShieldRegen.ToString();

        EcsEntity attacksEntity = _world.NewEntity();
        ref ActiveBulletsComponent _activeBullets = ref attacksEntity.Get<ActiveBulletsComponent>();
        _activeBullets.list = new List<BulletComponent>();

       

        EcsEntity damageTextEntity = _world.NewEntity();
        ref AllActiveDamageTextComponent activeDamageText = ref damageTextEntity.Get<AllActiveDamageTextComponent>();
        activeDamageText.damageTexts = new List<DamageTextRemoveTimeStruct>();
    
        EcsEntity lvlEntity = _world.NewEntity();
        ref var lvlComponent = ref lvlEntity.Get<LvlComponent>();
        lvlComponent.current = 1;
        ref var xpComponent = ref lvlEntity.Get<XpComponent>();
        _sceneData.xpSlider.value = xpComponent.current = 0;
        _sceneData.xpSlider.maxValue = xpComponent.max = _sceneData.lvlAndXpData.xpToReachLevel[lvlComponent.current];

    }
}
