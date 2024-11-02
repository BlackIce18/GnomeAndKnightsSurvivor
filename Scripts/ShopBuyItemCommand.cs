using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyItemCommand : ICommand
{
    private EcsFilter<WalletComponent> _walletComponent;
    private ShopItemData _shopItemData;
    public ShopBuyItemCommand(ShopItemData shopItem, EcsFilter<WalletComponent> wallet)
    {
        _shopItemData = shopItem;
        _walletComponent = wallet;
    }
    public void TryToBuyItem()
    {
        if (_walletComponent.Get1(0).money < _shopItemData.cost)
        {
            Debug.Log("Не хватает денег");
            return;
        }

        if (_shopItemData.cost > 0)
        {
            ref WalletComponent walletComponent = ref _walletComponent.Get1(0);
            walletComponent.money -= _shopItemData.cost; // Wallet индивидуальный т.е. не меняет UI
            ref WalletUpdateComponent walletUpdateComponent = ref _walletComponent.GetEntity(0).Get<WalletUpdateComponent>();
            walletUpdateComponent.money = walletComponent.money;
            walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
            walletUpdateComponent.killBounty = walletComponent.killBounty;
        }
    }

    public void Execute()
    {
        TryToBuyItem();
    }
}