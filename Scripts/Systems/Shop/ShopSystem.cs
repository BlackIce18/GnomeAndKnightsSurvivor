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

                ShopBuyItemCommand shopBuyItemCommand = new ShopBuyItemCommand(_sceneData.shop.ShopButtons, i, activeShopItemsComponent.shopItems[i], _filterWallet);

                _sceneData.shop.AddOnClick(_sceneData.shop.ShopButtons[i], shopBuyItemCommand);
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
            _elapsedTime = 0;
        }

        foreach(var i in _filterUpdateEvent)
        {
            ref var entity = ref _filterUpdateEvent.GetEntity(i);
            UpdateShopItems();
            entity.Del<ActiveShopItemsUpdateEventComponent>();
        }
    }

    private List<ShopItemData> UpdateShopItems()
    {
        _sceneData.shop.UpdateTimerSlider(_spawnTime);
        List<ShopItemData> items = new List<ShopItemData>();

        for (int i = 0; i < _sceneData.shop.ShopButtons.Count; i++)
        {
            ShopItemData shopItemData = GetRandomShopItem();
            items.Add(shopItemData);
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