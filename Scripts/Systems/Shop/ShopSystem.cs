using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class ShopSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;

    private EcsFilter<ActiveShopItemsComponent> _filter = null;
    private EcsFilter<ActiveShopItemsComponent, ActiveShopItemsUpdateEventComponent> _filterResetButtonPressedEvent = null;
    private EcsFilter<WalletComponent> _filterWallet = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private EcsFilter<ShopBuyItemCommandComponent, ShopBuyItemEventComponent> _filterShopBuyItemUpdateEvent = null;
    private EcsFilter<TimerComponent> _timerComponent = null;
    private float _spawnTime;
    private float _elapsedTime = 0;
    private EcsFilter<ResetShopComponent, ResetShopUpdateComponent> _resetShopComponentFilter = null;

    public void PreInit()
    {
        UpdateShop();
    }

    public void Init()
    {
        _spawnTime = _sceneData.shop.ResetShopData.ResetTimeSeconds;
        _sceneData.shop.UpdateTimerSlider(_spawnTime);
        _sceneData.shop.TimerSlider.maxValue = _sceneData.shop.ResetShopData.ResetTimeSeconds;
    }

    public void Run()
    {
        foreach(var i in _resetShopComponentFilter)
        {
            _sceneData.shop.UpdateTimerSlider(_spawnTime);
            _elapsedTime = 0;
        }
        _sceneData.shop.TimerSlider.value -= Time.deltaTime;
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            UpdateShop();
            _sceneData.shop.UpdateTimerSlider(_spawnTime);
            _sceneData.shop.ResetButton.Button.interactable = true;
            _elapsedTime = 0;
        }

        foreach(var i in _filterResetButtonPressedEvent)
        {
            ref var entity = ref _filterResetButtonPressedEvent.GetEntity(i);
            UpdateShop();
            entity.Del<ActiveShopItemsUpdateEventComponent>();
        }

        /*foreach(var i in _filterShopBuyItemUpdateEvent)
        {
            ref var entity = ref _filterShopBuyItemUpdateEvent.GetEntity(i);
            entity.Del<ShopBuyItemEventComponent>();
        }*/
    }

    private void UpdateShop()
    {
        //_sceneData.shop.UpdateTimerSlider(_spawnTime);
        List<ShopItemData> items = new List<ShopItemData>();
        List<ShopUIButton> shopButtons = _sceneData.shop.ShopButtons;

        for (int i = 0; i < shopButtons.Count; i++)
        {
            ResetButton(shopButtons[i]);

            ShopItemData shopItemData = GetRandomShopItem();
            items.Add(shopItemData);
            _sceneData.shop.ChangeImage(i, shopItemData.icon);
            _filterShopBuyItemComponent.Get2(0).isAvailable = true;

            ShopUIButton currentShopUIButton = shopButtons[i];
            ShopBuyCommand buyItemCommand = new ShopBuyCommand(currentShopUIButton, shopItemData, _filterShopBuyItemComponent.GetEntity(0), _filterWallet.GetEntity(0));
            _sceneData.shop.AddOnClick(currentShopUIButton, buyItemCommand);

            foreach (var j in _filterShopBuyItemComponent)
            {
                _filterShopBuyItemComponent.Get1(j).list.Add(buyItemCommand);
            }
        }

        /*foreach (var index in _filter)
        {
            ref ActiveShopItemsComponent activeShopItemsComponent = ref _filter.Get1(index);
            activeShopItemsComponent.shopItems = items;

            for (int i = 0; i < activeShopItemsComponent.shopItems.Count; i++)
            {
                ShopUIButton currentShopUIButton = _sceneData.shop.ShopButtons[i];
                ShopBuyItemCommand buyItemCommand = new ShopBuyItemCommand(currentShopUIButton, activeShopItemsComponent.shopItems[i], _filterShopBuyItemComponent.GetEntity(0), _filterWallet.GetEntity(0));

                _sceneData.shop.AddOnClick(currentShopUIButton, buyItemCommand);

                foreach (var j in _filterShopBuyItemComponent)
                {
                    _filterShopBuyItemComponent.Get1(j).list.Add(buyItemCommand);
                }
            }
        }*/
    }
    private void ResetButton(ShopUIButton button)
    {
        button.Button.interactable = true;
        var color = button.Image.color;
        color.a = 1f;
        button.Image.color = color;
    }

    private void UpdateShopItem(int index, ShopItemGunData item)
    {
        foreach (var shopComponent in _filter)
        {
            _filter.GetEntity(shopComponent).Get<ActiveShopItemsComponent>().shopItems[index] = item;
        }
        _sceneData.shop.ChangeImage(index, item.icon);
    }

    private ShopItemData GetRandomShopItem()
    {
        return _sceneData.shop.ItemsData[UnityEngine.Random.Range(0, _sceneData.shop.ItemsData.Count)];
    }
}