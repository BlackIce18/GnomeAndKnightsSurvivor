using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world;
    private EcsFilter<ActiveShopItemsComponent> _filter = null;
    private EcsFilter<ActiveShopItemsComponent, ActiveShopItemsUpdateEventComponent> _filterResetButtonPressedEvent = null;
    private EcsFilter<WalletComponent> _filterWallet = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
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
    }

    private void UpdateShop()
    {
        List<ShopItemData> items = new List<ShopItemData>();
        List<ShopUIButton> shopButtons = _sceneData.shop.ShopButtons;

        for (int i = 0; i < shopButtons.Count; i++)
        {
            ResetButton(shopButtons[i]);
            Debug.Log("1");
            ShopItemData shopItemData = GetRandomShopItem();
            items.Add(shopItemData);
            _sceneData.shop.ChangeImage(i, shopItemData.icon);
            _filterShopBuyItemComponent.Get2(0).isAvailable = true;

            ShopUIButton currentShopUIButton = shopButtons[i];

            ShopBuyCommand buyItemCommand = new ShopBuyCommand(currentShopUIButton, shopItemData, _filterShopBuyItemComponent.GetEntity(0), _filterWallet.GetEntity(0));
            _sceneData.shop.AddOnClick(currentShopUIButton, buyItemCommand);

            EcsEntity borderColorEventEntity = _world.NewEntity();
            ref BorderColorComponent borderColorEvent = ref borderColorEventEntity.Get<BorderColorComponent>();
            borderColorEvent.shopUIButton = currentShopUIButton;
            borderColorEvent.rarity = shopItemData.rarity;
            borderColorEventEntity.Get<BorderUpdateColorEventComponent>();

            foreach (var j in _filterShopBuyItemComponent)
            {
                _filterShopBuyItemComponent.Get1(j).list.Add(buyItemCommand);
            }
        }
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