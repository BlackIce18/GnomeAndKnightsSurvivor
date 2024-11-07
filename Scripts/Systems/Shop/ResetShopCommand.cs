using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public struct ResetShopCommandComponent
{
    public ResetShopCommand resetShopCommand;
    public int price;
}
public class ResetShopCommand : ICommand
{
    private EcsFilter<ActiveShopItemsComponent> _activeShopItemsComponent;
    private List<ShopUIButton> _shopButtons;
    private int _rollsCount = 1;
    private readonly int _freeStartResetsCount = 5;
    private readonly int _freeBuyTime = 10;
    private int _resetPrice = 0;
    private int _resetPriceIncrease = 100;
    private ShopUIButton _shopUIButton;
    private EcsEntity _wallet;
    private EcsEntity _time;
    public ResetShopCommand(EcsEntity time, ShopUIButton shopUIButton, List<ShopUIButton> shopButtons, EcsFilter<ActiveShopItemsComponent> activeShopItemsComponent, EcsEntity wallet)
    {
        _time = time;
        _shopUIButton = shopUIButton;
        _shopButtons = shopButtons;
        _activeShopItemsComponent = activeShopItemsComponent;
        _wallet = wallet;
    }
    private void ResetShopItems()
    {
        foreach (var i in _activeShopItemsComponent)
        {
            ref ActiveShopItemsComponent activeShopItemsComponent = ref _activeShopItemsComponent.Get1(i);
            ref var entityActiveShopItems = ref _activeShopItemsComponent.GetEntity(i);

            ref ActiveShopItemsUpdateEventComponent activeShopItemsUpdateEventComponent = ref entityActiveShopItems.Get<ActiveShopItemsUpdateEventComponent>();
            activeShopItemsUpdateEventComponent.shopItems = activeShopItemsComponent.shopItems;
        }

        foreach(ShopUIButton shopUIButton in _shopButtons) 
        {
            shopUIButton.Button.interactable = true;
        }
    }

    private void Reset()
    {
        if((_time.Get<TimerComponent>().minutes == 0) && (_time.Get<TimerComponent>().seconds <= _freeBuyTime) && (_time.Get<TimerComponent>().hours == 0))
        {
            if ((_rollsCount <= _freeStartResetsCount))
            {
                ResetShopItems();
                _rollsCount++;
            }
            if(_rollsCount == _freeStartResetsCount)
            {
                _shopUIButton.Button.interactable = false;
            }
            return;
        }

        ref WalletComponent walletComponent = ref _wallet.Get<WalletComponent>();
        ref WalletUpdateComponent walletUpdateComponent = ref _wallet.Get<WalletUpdateComponent>();
        Debug.Log(walletComponent.money);

        if(_shopUIButton.Button.interactable == false)
        {
            _shopUIButton.Button.interactable = true;
        }

        if (walletComponent.money - _resetPrice >= _resetPrice)
        {
            ResetShopItems();
            walletComponent.money -= _resetPrice;
            walletUpdateComponent.money = walletComponent.money;
            walletUpdateComponent.killBounty = walletComponent.killBounty;
            walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;

            _resetPrice += _resetPriceIncrease;
        }
        else
        {
            Debug.Log("Не хватает денег!");
        }
    }
    public void Execute()
    {
        Reset();
    }
}
