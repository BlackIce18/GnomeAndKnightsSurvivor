using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ShopPurchaseCommand<T> : ICommand where T : ShopItemData
{
    private protected ShopItemData _shopItemData;
    private protected ShopUIButton _shopUIButton;
    private protected EcsEntity _walletEntity;
    private protected EcsEntity _shopEntity;
    public ShopPurchaseCommand(ShopUIButton shopUIButton, T shopItemData, EcsEntity shopEntity, EcsEntity walletEntity)
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

    public abstract void Execute();
}
public class ShopBuyCommand : ShopPurchaseCommand<ShopItemData>, ICommand
{
    public ShopBuyCommand(ShopUIButton shopUIButton, ShopItemData shopGunData, EcsEntity shopEntity, EcsEntity walletEntity)
        : base(shopUIButton, shopGunData, shopEntity, walletEntity)
    {
    }
    public void Update(ShopUIButton shopUIButton, ShopItemData shopItemData, EcsEntity shopEntity, EcsEntity walletEntity)
    {
        _shopUIButton = shopUIButton;
        _shopItemData = shopItemData;
        _shopEntity = shopEntity;
        _walletEntity = walletEntity;
    }

    public override void Execute()
    {
        ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
        ref ShopBuyItemCommandComponent shopBuyItemComponent = ref _shopEntity.Get<ShopBuyItemCommandComponent>();
            
        if(_shopUIButton.Button.interactable)
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

                Color color = _shopUIButton.Image.color;
                color.a = 0f;
                _shopUIButton.Image.color = color;
                _shopUIButton.Border.gameObject.SetActive(false);
                
                _shopEntity.Get<ShopBuyItemEventComponent>().item = _shopItemData;
                _shopEntity.Get<PurchasedItemsComponent>().items.Add(_shopItemData);
                shopBuyItemComponent.isAvailable = false;
            }
        }
    }
}