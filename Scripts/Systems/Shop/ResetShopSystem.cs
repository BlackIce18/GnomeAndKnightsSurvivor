using Leopotam.Ecs;
using UnityEngine;
public class ResetShopSystem : IEcsPreInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<TimerComponent> _timerFilter = null;
    private EcsFilter<WalletComponent> _walletFilter = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private EcsFilter<ResetShopComponent, ResetShopUpdateEventComponent> _resetShopUpdateEventComponentFilter = null;
    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter = null;

    public void PreInit()
    {

    }

    public void Run()
    {
        foreach (var i in _resetShopUpdateEventComponentFilter)
        {
            ref EcsEntity entity = ref _resetShopUpdateEventComponentFilter.GetEntity(i);
            /*
            ref ResetShopComponent resetShopComponent = ref _resetShopUpdateEventComponentFilter.Get1(i);
            ref var currentTime = ref _timerFilter.Get1(0);
            Debug.Log("2");

            if ((currentTime.seconds <= _sceneData.shop.ResetShopData.FreeBuyTime) && (currentTime.minutes == 0) && (currentTime.hours == 0))
            {
                bool itemPurchased = false;

                if (_purchasedItemsFilter.Get1(0).items.Count > 0)
                {
                    itemPurchased = true;
                }

                if (!itemPurchased && resetShopComponent.isAvailable)
                {
                    resetShopComponent.currentResetPrice = 0;
                }

                if (resetShopComponent.isAvailable && (resetShopComponent.rollsCount > _sceneData.shop.ResetShopData.FreeStartResetsCount))
                {
                    resetShopComponent.isAvailable = false;
                    _sceneData.shop.ResetButton.Button.interactable = false;
                }
            }

            _sceneData.shop.UpdateResetPrice(resetShopComponent.currentResetPrice);*/
            entity.Del<ResetShopUpdateEventComponent>();
        }
    }


}
