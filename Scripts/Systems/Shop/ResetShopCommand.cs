using Leopotam.Ecs;
using UnityEngine;

public class ResetShopCommand : ICommand
{
    private EcsEntity _walletEntity;
    private EcsEntity _shopEntity;
    private SceneData _sceneData;
    private EcsEntity _timerEntity;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private PurchasedItemsComponent _purchasedItemsComponent;

    public ResetShopCommand(
        SceneData sceneData,
        EcsEntity shopEntity,
        EcsEntity walletEntity,
        EcsEntity timerEntity,
        PurchasedItemsComponent purchasedItemsComponent)
    {
        _walletEntity = walletEntity;
        _shopEntity = shopEntity;
        _sceneData = sceneData;

        _timerEntity = timerEntity;
        _purchasedItemsComponent = purchasedItemsComponent;
    }
    public void Execute()
    {
        ref var resetShopComponent = ref _shopEntity.Get<ResetShopComponent>();

        if (resetShopComponent.isAvailable == false)
        {
            Debug.Log("Return");
            return;
        }

        var currentTime = _timerEntity.Get<TimerComponent>();
        if((_purchasedItemsComponent.items.Count == 0))
        {
            if ((currentTime.seconds <= _sceneData.shop.ResetShopData.FreeBuyTime) && (currentTime.minutes == 0) && (currentTime.hours == 0))
            {
                if (((resetShopComponent.rollsCount - 1 < _sceneData.shop.ResetShopData.FreeStartResetsCount)))
                {
                    resetShopComponent.currentResetPrice = 0;
                }

                if ((resetShopComponent.rollsCount >= _sceneData.shop.ResetShopData.FreeStartResetsCount))
                {
                    resetShopComponent.isAvailable = false;
                    HideButton();
                }
            }
        }


        ref WalletComponent walletComponent = ref _walletEntity.Get<WalletComponent>();
        if ((walletComponent.money - resetShopComponent.currentResetPrice) > 0)
        {
            UpdateWallet(ref _walletEntity.Get<WalletComponent>(), resetShopComponent.currentResetPrice);

            if((_purchasedItemsComponent.items.Count > 0) || (resetShopComponent.rollsCount - 1 >= _sceneData.shop.ResetShopData.FreeStartResetsCount))
            {
                resetShopComponent.currentResetPrice += _sceneData.shop.ResetShopData.ResetPriceIncrease;
            }
            _sceneData.shop.UpdateResetPrice(resetShopComponent.currentResetPrice);
            ResetShopItems();
            _shopEntity.Get<ResetShopUpdateEventComponent>();
            resetShopComponent.rollsCount++;
        }
        else
        {
            Debug.Log("Не хватает денег!");
        }
    }

    private void UpdateWallet(ref WalletComponent walletComponent, int price)
    {
        ref WalletUpdateComponent walletUpdateComponent = ref _walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money -= price;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
    }

    private void ResetShopItems()
    {
        ref var _activeShopItemsComponent = ref _shopEntity.Get<ShopBuyCommandsComponent>();
        ref ShopBuyCommandsUpdateEventComponent activeShopItemsUpdateEventComponent = ref _shopEntity.Get<ShopBuyCommandsUpdateEventComponent>();
        ref ShopBuyItemCommandComponent shopItems = ref _shopEntity.Get<ShopBuyItemCommandComponent>();


        //activeShopItemsUpdateEventComponent.shopItems = _activeShopItemsComponent.shopItems;

        foreach (ShopUIButton shopUIButton in _sceneData.shop.ShopButtons)
        {
            shopUIButton.Button.interactable = true;
            var color = shopUIButton.Image.color;
            color.a = 1f;
            shopUIButton.Image.color = color;
            shopUIButton.Border.gameObject.SetActive(true);
        }
    }


    private void HideButton()
    {
        _sceneData.shop.ResetButton.Button.interactable = false;
    }
}
