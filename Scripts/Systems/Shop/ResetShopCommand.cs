using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public struct ResetShopCommandComponent
{
    public ResetShopCommand resetShopCommand;
}
public class ResetShopCommand : ICommand
{
    private EcsFilter<ActiveShopItemsComponent> _activeShopItemsComponent;
    private EcsFilter<WalletComponent> _wallet;
    private List<ShopUIButton> _shopButtons;
    public ResetShopCommand(List<ShopUIButton> shopButtons, EcsFilter<ActiveShopItemsComponent> activeShopItemsComponent, EcsFilter<WalletComponent> wallet)
    {
        _activeShopItemsComponent = activeShopItemsComponent;
        _wallet = wallet;
        _shopButtons = shopButtons;
    }
    private void ResetShop()
    {
        foreach (var i in _activeShopItemsComponent)
        {
            ref ActiveShopItemsComponent activeShopItemsComponent = ref _activeShopItemsComponent.Get1(i);
            ref var entityActiveShopItems = ref _activeShopItemsComponent.GetEntity(i);

            ref ActiveShopItemsUpdateEventComponent activeShopItemsUpdateEventComponent = ref entityActiveShopItems.Get<ActiveShopItemsUpdateEventComponent>();
            activeShopItemsUpdateEventComponent.shopItems = activeShopItemsComponent.shopItems;
        }

        foreach(ShopUIButton shopUIButton in _shopButtons) {
            shopUIButton.Button.interactable = true;
        }
    }
    public void Execute()
    {
        ResetShop();
    }
}
