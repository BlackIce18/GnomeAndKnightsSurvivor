using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

public class ResetShopCommand : ICommand
{
    private EcsEntity _walletEntity;
    private EcsEntity _shopEntity;
    private SceneData _sceneData;

    public ResetShopCommand(SceneData sceneData,
        EcsEntity shopEntity,
        EcsEntity walletEntity)
    {
        _walletEntity = walletEntity;
        _shopEntity = shopEntity;
        _sceneData = sceneData;
    }
    private void ResetShopItems()
    {
        ref var _activeShopItemsComponent = ref _shopEntity.Get<ActiveShopItemsComponent>();
        ref ActiveShopItemsUpdateEventComponent activeShopItemsUpdateEventComponent = ref _shopEntity.Get<ActiveShopItemsUpdateEventComponent>();
        activeShopItemsUpdateEventComponent.shopItems = _activeShopItemsComponent.shopItems;

        foreach (ShopUIButton shopUIButton in _sceneData.shop.ShopButtons) 
        {
            shopUIButton.Button.interactable = true;
        }
    }
    private void UpdateWallet(ref WalletComponent walletComponent, ref ResetShopComponent resetShopComponent)
    {
        ref WalletUpdateComponent walletUpdateComponent = ref _walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money -= resetShopComponent.currentResetPrice;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
    }

    private void Reset()
    {
        ref var resetShopComponent = ref _shopEntity.Get<ResetShopComponent>();
        
        if(resetShopComponent.isAvailable)
        {
            ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
            if ((walletComponent.money - resetShopComponent.currentResetPrice) >= 0)
            {
                UpdateWallet(ref walletComponent, ref resetShopComponent);
                resetShopComponent.currentResetPrice += _sceneData.shop.ResetShopData.ResetPriceIncrease;
                _shopEntity.Get<ResetShopUpdateComponent>();
                
                ResetShopItems();
                resetShopComponent.rollsCount++;
            }
            else
            {
                Debug.Log("Не хватает денег!");
            }
        }




        /*
        ref var currentTime = ref _timerEntity.Get<TimerComponent>();

        if ((currentTime.minutes == 0) && (currentTime.seconds <= _shop.ResetShopData.FreeBuyTime) && (currentTime.hours == 0))
        {
            ref var resetShopComponent = ref _shopEntity.Get<ResetShopComponent>();
            ref var ResetShopUpdateComponent = ref _shopEntity.Get<ResetShopUpdateComponent>();

            if ((resetShopComponent.rollsCount <= _shop.ResetShopData.FreeStartResetsCount))
            {
                ResetShopItems();
                resetShopComponent.rollsCount++;
                return;
            }
            if ((resetShopComponent.rollsCount - 1) == _shop.ResetShopData.FreeStartResetsCount)
            {
                resetShopComponent.isInteractiveButton = false;
                //_shopUIButton.Button.interactable = false;
                return;
            }
        }
        */

    }
    public void Execute()
    {
        Reset();
    }
}
