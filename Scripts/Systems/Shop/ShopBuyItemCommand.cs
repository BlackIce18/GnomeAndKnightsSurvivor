using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBuyItemCommand : ICommand
{
    private ShopItemData _shopItemData;
    private ShopUIButton _shopUIButton;
    private EcsEntity _walletEntity;
    private EcsEntity _shopEntity;

    public ShopBuyItemCommand(ShopUIButton shopUIButton, ShopItemData shopItem, EcsEntity shopEntity, EcsEntity walletEntity)
    {
        _shopUIButton = shopUIButton;
        _shopItemData = shopItem;
        _shopEntity = shopEntity;
        _walletEntity = walletEntity;
    }
    private void UpdateWallet(ref WalletComponent walletComponent, int cost)
    {
        ref WalletUpdateComponent walletUpdateComponent = ref _walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money -= cost;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
    }
    private void TryToBuyItem()
    {
        ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
        ref ShopBuyItemCommandComponent shopBuyItemComponent = ref _shopEntity.Get<ShopBuyItemCommandComponent>();
        
        if(shopBuyItemComponent.isAvailable)
        {
            if (walletComponent.money < _shopItemData.cost)
            {
                Debug.Log("Не хватает денег");
                return;
            }

            if (_shopItemData.cost > 0)
            {
                UpdateWallet(ref walletComponent, _shopItemData.cost);

                _shopUIButton.Button.interactable = false;
                _shopUIButton.Image.sprite = null;
                _shopEntity.Get<ShopBuyItemEventComponent>().item = _shopItemData;
                _shopEntity.Get<PurchasedItemsComponent>().items.Add(_shopItemData);

                // !!!!!!!!!!!!!!
                /*ref PurchasedItemsComponent purchasedItemsComponent = ref _shopEntity.Get<PurchasedItemsComponent>();
                purchasedItemsComponent.items = new List<ShopItemData>() { _shopItemData };*/
                // !!!!!!!!!!!!!!
            }
        }
    }

    public void Execute()
    {
        TryToBuyItem();
    }
}