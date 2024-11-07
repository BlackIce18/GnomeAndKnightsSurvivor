using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyItemCommand : ICommand
{
    private EcsFilter<WalletComponent> _wallet;
    private ShopItemData _shopItemData;
    private int _buttonIndex;
    private List<ShopUIButton> _shopButtons;
    public ShopBuyItemCommand(List<ShopUIButton> shopButtons, int buttonIndex, ShopItemData shopItem, EcsFilter<WalletComponent> wallet)
    {
        _shopItemData = shopItem;
        _wallet = wallet;
        _buttonIndex = buttonIndex;
        _shopButtons = shopButtons;
    }
    private void TryToBuyItem()
    {
        if (_wallet.Get1(0).money < _shopItemData.cost)
        {
            Debug.Log("Не хватает денег");
            return;
        }

        if (_shopItemData.cost > 0)
        {
            ref WalletComponent walletComponent = ref _wallet.Get1(0);
            walletComponent.money -= _shopItemData.cost;
            ref WalletUpdateComponent walletUpdateComponent = ref _wallet.GetEntity(0).Get<WalletUpdateComponent>();
            walletUpdateComponent.money = walletComponent.money;
            walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
            walletUpdateComponent.killBounty = walletComponent.killBounty;

            _shopButtons[_buttonIndex].Button.interactable = false;
            _shopButtons[_buttonIndex].Image.sprite = null;
        }
    }

    public void Execute()
    {
        TryToBuyItem();
    }
}