using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;

    private EcsFilter<ActiveShopItemsComponent> _filter = null;
    private EcsFilter<ActiveShopItemsComponent, ActiveShopItemsUpdateEventComponent> _filterUpdateEvent = null;
    private EcsFilter<WalletComponent> _filterWallet = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private EcsFilter<ShopBuyItemCommandComponent, ShopBuyItemEventComponent> _filterShopBuyItemUpdateEvent = null;
    private EcsFilter<TimerComponent> _timerComponent = null;
    private float _spawnTime;
    private float _elapsedTime = 0;
    private EcsFilter<ResetShopComponent, ResetShopUpdateComponent> _resetShopComponentFilter = null;

    public void PreInit()
    {
        UpdateShopItems();

        foreach(var index in _filter)
        {
            ref ActiveShopItemsComponent activeShopItemsComponent = ref _filter.Get1(index);

            for (int i = 0; i < activeShopItemsComponent.shopItems.Count; i++)
            {
                ShopUIButton currentShopUIButton = _sceneData.shop.ShopButtons[i];
                ShopItemData shopItem = activeShopItemsComponent.shopItems[i];
                EcsEntity shopEntity = _filterShopBuyItemComponent.GetEntity(0);
                EcsEntity walletEntity = _filterWallet.GetEntity(0);
                ShopBuyItemCommand shopBuyItemCommand = new ShopBuyItemCommand(currentShopUIButton, shopItem, shopEntity, walletEntity);

                _sceneData.shop.AddOnClick(currentShopUIButton, shopBuyItemCommand);
                foreach (var j in _filterShopBuyItemComponent)
                {
                    _filterShopBuyItemComponent.Get1(j).list.Add(shopBuyItemCommand);
                }
            }
        }
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
            UpdateShopItems();
            _sceneData.shop.ResetButton.Button.interactable = true;
            _elapsedTime = 0;
        }

        foreach(var i in _filterUpdateEvent)
        {
            ref var entity = ref _filterUpdateEvent.GetEntity(i);
            UpdateShopItems();
            entity.Del<ActiveShopItemsUpdateEventComponent>();
        }

        /*foreach(var i in _filterShopBuyItemUpdateEvent)
        {
            ref var entity = ref _filterShopBuyItemUpdateEvent.GetEntity(i);
            entity.Del<ShopBuyItemEventComponent>();
        }*/
    }

    private List<ShopItemData> UpdateShopItems()
    {
        _sceneData.shop.UpdateTimerSlider(_spawnTime);
        List<ShopItemData> items = new List<ShopItemData>();

        List<ShopUIButton> UIbuttons = _sceneData.shop.ShopButtons;
        for (int i = 0; i < UIbuttons.Count; i++)
        {
            ShopItemData shopItemData = GetRandomShopItem();
            items.Add(shopItemData);

            UIbuttons[i].Button.interactable = true;
            _filterShopBuyItemComponent.Get2(0).isAvailable = true;

            _sceneData.shop.ChangeImage(i, shopItemData.icon);
        }

        foreach (var shopComponent in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(shopComponent);
            entity.Get<ActiveShopItemsComponent>().shopItems = items;
        }

        return items;
    }

    private void UpdateShopItem(int index, ShopItemData newShopItemData)
    {
        foreach (var shopComponent in _filter)
        {
            ref EcsEntity entity = ref _filter.GetEntity(shopComponent);
            var shopItemData = entity.Get<ActiveShopItemsComponent>().shopItems[index] = newShopItemData;
        }
        _sceneData.shop.ChangeImage(index, newShopItemData.icon);
    }

    private ShopItemData GetRandomShopItem()
    {
        int randomNumber = Random.Range(0, _sceneData.shop.ItemsData.Count);
        return GetShopItem(randomNumber);
    }

    private ShopItemData GetShopItem(int index)
    {
        return _sceneData.shop.ItemsData[index];
    }
}