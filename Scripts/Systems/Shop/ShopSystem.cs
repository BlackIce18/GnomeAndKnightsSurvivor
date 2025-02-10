using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
public class ShopSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world;
    private EcsFilter<ShopBuyCommandsComponent> _shopBuyCommandsfilter;
    private EcsFilter<ShopBuyCommandsComponent, ShopBuyCommandsUpdateEventComponent> _filterResetButtonPressedEvent = null;
    private EcsFilter<WalletComponent> _filterWallet = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private float _resetTimeSeconds;
    private float _elapsedTime = 0;
    private EcsFilter<ResetShopComponent, ResetShopUpdateEventComponent> _resetShopComponentFilter = null;

    public void PreInit()
    {
        //Update();
    }

    public void Init()
    {
        _resetTimeSeconds = _sceneData.shop.ResetShopData.ResetTimeSeconds;
        _sceneData.shop.UpdateTimerSlider(_resetTimeSeconds);
        _sceneData.shop.TimerSlider.maxValue = _resetTimeSeconds;
        
        Update();
    }

    public void Run()
    {
        foreach(var i in _resetShopComponentFilter)
        {
            _sceneData.shop.UpdateTimerSlider(_resetTimeSeconds);
            _elapsedTime = 0;
        }
        _sceneData.shop.TimerSlider.value -= Time.deltaTime;
        if ((_elapsedTime += Time.deltaTime) >= _resetTimeSeconds)
        {
            Update();
            _sceneData.shop.UpdateTimerSlider(_resetTimeSeconds);
            _sceneData.shop.ResetButton.Button.interactable = true;
            _filterShopBuyItemComponent.Get2(0).isAvailable = true;
            _elapsedTime = 0;
        }

        foreach(var i in _filterResetButtonPressedEvent)
        {
            ref var entity = ref _filterResetButtonPressedEvent.GetEntity(i);
            Update();
            entity.Del<ShopBuyCommandsUpdateEventComponent>();
        }
    }
    private void Update()
    {
        ref var shopEntity = ref _filterShopBuyItemComponent.GetEntity(0);
        ref var walletEntity = ref _filterWallet.GetEntity(0);
        foreach (var index in _shopBuyCommandsfilter)
        {
            ref var shopBuyCommandComponent = ref _shopBuyCommandsfilter.Get1(index);
            if(shopBuyCommandComponent.buyCommands.Count == 0)
            {
                InitShop(ref shopBuyCommandComponent);
                continue;
            }
            for(int i = 0; i < shopBuyCommandComponent.buyCommands.Count; i++)
            {
                ShopBuyCommand shopBuyCommand = shopBuyCommandComponent.buyCommands[i];

                var itemData = _sceneData.shop.ItemsData[UnityEngine.Random.Range(0, _sceneData.shop.ItemsData.Count)];
                _sceneData.shop.ChangeImage(i, itemData.icon);

                ShopBuyCommand buyItemCommand = shopBuyCommandComponent.buyCommands[i];
                shopBuyCommandComponent.buyCommands[i] = buyItemCommand;

                shopBuyCommand.Update(_sceneData.shop.ShopButtons[i], itemData, shopEntity, walletEntity);
                _sceneData.shop.AddOnClick(_sceneData.shop.ShopButtons[i], buyItemCommand);

                EcsEntity borderColorEventEntity = _world.NewEntity();
                ref BorderColorComponent borderColorEvent = ref borderColorEventEntity.Get<BorderColorComponent>();
                borderColorEvent.shopUIButton = _sceneData.shop.ShopButtons[i];
                borderColorEvent.rarity = itemData.rarity;
                borderColorEventEntity.Get<BorderUpdateColorEventComponent>();

                foreach (var j in _filterShopBuyItemComponent)
                {
                    _filterShopBuyItemComponent.Get1(j).list.Add(buyItemCommand);
                }
            }
            foreach (ShopUIButton shopUIButton in _sceneData.shop.ShopButtons)
            {
                shopUIButton.Button.interactable = true;
                var color = shopUIButton.Image.color;
                color.a = 1f;
                shopUIButton.Image.color = color;
                shopUIButton.Border.gameObject.SetActive(true);
            }
        }

    }

    private void InitShop(ref ShopBuyCommandsComponent shopBuyCommandComponent)
    {
        List<ShopItemData> items = new List<ShopItemData>();
        List<ShopUIButton> shopButtons = _sceneData.shop.ShopButtons;

        for (int i = 0; i < shopButtons.Count; i++)
        {
            //ResetButton(shopButtons[i]);
            items.Add(_sceneData.shop.ItemsData[UnityEngine.Random.Range(0, _sceneData.shop.ItemsData.Count)]);
            _sceneData.shop.ChangeImage(i, items[items.Count-1].icon);
            // Убрать создание новых ShopBuyCommand
            ShopBuyCommand buyItemCommand = new ShopBuyCommand(shopButtons[i], items[items.Count - 1], _filterShopBuyItemComponent.GetEntity(0), _filterWallet.GetEntity(0));
            shopBuyCommandComponent.buyCommands.Add(buyItemCommand);
            _sceneData.shop.AddOnClick(shopButtons[i], buyItemCommand);

            EcsEntity borderColorEventEntity = _world.NewEntity();
            ref BorderColorComponent borderColorEvent = ref borderColorEventEntity.Get<BorderColorComponent>();
            borderColorEvent.shopUIButton = shopButtons[i];
            borderColorEvent.rarity = items[items.Count - 1].rarity;
            borderColorEventEntity.Get<BorderUpdateColorEventComponent>();

            foreach (var j in _filterShopBuyItemComponent)
            {
                _filterShopBuyItemComponent.Get1(j).list.Add(buyItemCommand);
            }
        }
    }
}