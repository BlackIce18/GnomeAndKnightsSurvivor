using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ShopSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private Shop _shop;

    private EcsFilter<ActiveShopItemsComponent> _filter = null;
    private EcsFilter<ActiveShopItemsComponent, ActiveShopItemsUpdateEventComponent> _filterUpdateEvent = null;
    private EcsFilter<WalletComponent> _filterWallet = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopCommandComponent> _filterShopBuyItemComponent = null;
    private EcsFilter<TimerComponent> _timerComponent = null;
    private float _spawnTime;
    private float _elapsedTime = 0;

    public void PreInit()
    {
        _shop = _sceneData.shop;
        UpdateShopItems();
        ResetShopCommand resetCommand = new ResetShopCommand(_timerComponent.GetEntity(0), _shop.ResetButton,  _shop.ShopButtons, _filter, _filterWallet.GetEntity(0));
        _shop.AddOnClick(_shop.ResetButton, resetCommand);

        foreach(var index in _filter)
        {
            ref ActiveShopItemsComponent activeShopItemsComponent = ref _filter.Get1(index);

            for (int i = 0; i < activeShopItemsComponent.shopItems.Count; i++)
            {

                ShopBuyItemCommand shopBuyItemCommand = new ShopBuyItemCommand(_shop.ShopButtons, i, activeShopItemsComponent.shopItems[i], _filterWallet);
                
                _shop.AddOnClick(_shop.ShopButtons[i], shopBuyItemCommand);
                foreach (var j in _filterShopBuyItemComponent)
                {
                    ref var entity = ref _filterShopBuyItemComponent.GetEntity(j);
                    entity.Get<ResetShopCommandComponent>().resetShopCommand = resetCommand;
                    _filterShopBuyItemComponent.Get1(j).list.Add(shopBuyItemCommand);
                }
            }
        }
    }

    public void Init()
    {
        _spawnTime = 60f;
        _shop.TimerSlider.value = _spawnTime;
    }

    public void Run()
    {
        _shop.TimerSlider.value -= Time.deltaTime;
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            UpdateShopItems();
            _elapsedTime = 0;
            _shop.TimerSlider.value = _spawnTime;
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
        _shop.TimerSlider.value = _spawnTime;
        List<ShopItemData> items = new List<ShopItemData>();

        for (int i = 0; i < _shop.ShopButtons.Count; i++)
        {
            ShopItemData shopItemData = GetRandomShopItem();
            items.Add(shopItemData);
            _shop.ChangeImage(i, shopItemData.icon);
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
            var a = entity.Get<ActiveShopItemsComponent>().shopItems[index] = newShopItemData;
        }
        _shop.ChangeImage(index, newShopItemData.icon);
    }

    private ShopItemData GetRandomShopItem()
    {
        int randomNumber = Random.Range(0, _shop.ItemsData.Count);
        return GetShopItem(randomNumber);
    }

    private ShopItemData GetShopItem(int index)
    {
        return _shop.ItemsData[index];
    }
}