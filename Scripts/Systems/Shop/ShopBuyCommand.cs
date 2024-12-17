using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ShopPurchaseCommand
{
    private protected ShopItemData _shopItemData;
    private protected ShopUIButton _shopUIButton;
    private protected EcsEntity _walletEntity;
    private protected EcsEntity _shopEntity;
    public ShopPurchaseCommand(ShopUIButton shopUIButton, ShopItemData shopItemData, EcsEntity shopEntity, EcsEntity walletEntity)
    {
        _shopUIButton = shopUIButton;
        _shopItemData = shopItemData;
        _shopEntity = shopEntity;
        _walletEntity = walletEntity;
    }
    private protected void UpdateWallet(ref WalletComponent walletComponent, int cost)
    {
        ref WalletUpdateComponent walletUpdateComponent = ref _walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money -= cost;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
    }
}
public class ShopBuyCommand : ShopPurchaseCommand, ICommand
{
    private bool _canBuy = true;

    public ShopBuyCommand(ShopUIButton shopUIButton, ShopItemData shopItemData, EcsEntity shopEntity, EcsEntity walletEntity) : base(shopUIButton, shopItemData, shopEntity, walletEntity)
    {
    }

    public void Execute()
    {
        if (_canBuy)
        {
            ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
            ref ShopBuyItemCommandComponent shopBuyItemComponent = ref _shopEntity.Get<ShopBuyItemCommandComponent>();
            
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

                Color color = _shopUIButton.Image.color;
                color.a = 0f;
                _shopUIButton.Image.color = color;

                _shopEntity.Get<ShopBuyItemEventComponent>().item = _shopItemData;
                _shopEntity.Get<PurchasedItemsComponent>().items.Add(_shopItemData);
                _canBuy = false;
                // !!!!!!!!!!!!!!
                /*ref PurchasedItemsComponent purchasedItemsComponent = ref _shopEntity.Get<PurchasedItemsComponent>();
                purchasedItemsComponent.items = new List<ShopItemData>() { _shopItemData };*/
                // !!!!!!!!!!!!!!
            }
        }
    }
}

public class ShopBuyBuffCommand : ShopPurchaseCommand, ICommand
{
    private bool _canBuy = true;
    public ShopBuyBuffCommand(ShopUIButton shopUIButton, ShopItemGunData shopItemData, EcsEntity shopEntity, EcsEntity walletEntity) : base(shopUIButton, shopItemData, shopEntity, walletEntity)
    {
    }

    public void Execute()
    {
        if (_canBuy)
        {
            ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
            ref ShopBuyItemCommandComponent shopBuyItemComponent = ref _shopEntity.Get<ShopBuyItemCommandComponent>();

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

                Color color = _shopUIButton.Image.color;
                color.a = 0f;
                _shopUIButton.Image.color = color;


                _shopEntity.Get<ShopBuyItemEventComponent>().item = _shopItemData;
                _shopEntity.Get<PurchasedItemsComponent>().items.Add(_shopItemData);
                _canBuy = false;
                // !!!!!!!!!!!!!!
                /*ref PurchasedItemsComponent purchasedItemsComponent = ref _shopEntity.Get<PurchasedItemsComponent>();
                purchasedItemsComponent.items = new List<ShopItemData>() { _shopItemData };*/
                // !!!!!!!!!!!!!!
            }
        }
    }
}