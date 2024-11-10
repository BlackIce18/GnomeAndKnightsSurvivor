using Leopotam.Ecs;
using System.Diagnostics;
using UnityEngine;

public class ResetShopSystem : IEcsPreInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private int _rollsCount = 1;
    private EcsFilter<TimerComponent> _timerFilter = null;
    private EcsFilter<WalletComponent> _walletFilter = null;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;
    private EcsFilter<ResetShopComponent, ResetShopUpdateComponent> _resetShopComponentFilter = null;

    public void PreInit()
    {
        ResetShopCommand resetCommand = new ResetShopCommand(_sceneData, _filterShopBuyItemComponent.GetEntity(0), _walletFilter.GetEntity(0));
        _sceneData.shop.AddOnClick(_sceneData.shop.ResetButton, resetCommand);
        foreach(var i in _filterShopBuyItemComponent)
        {
            ref var resetComponent = ref _filterShopBuyItemComponent.Get2(i);
            resetComponent.resetShopCommand = resetCommand;
        }
    }

    public void Run()
    {
        ref var currentTime = ref _timerFilter.Get1(0);

        foreach (var i in _resetShopComponentFilter)
        {
            ref ResetShopComponent resetShopComponent = ref _resetShopComponentFilter.Get1(i);
            ref EcsEntity entity = ref _resetShopComponentFilter.GetEntity(i);

            if ((currentTime.minutes == 0) && (currentTime.seconds <= _sceneData.shop.ResetShopData.FreeBuyTime) && (currentTime.hours == 0))
            {
                resetShopComponent.currentResetPrice = 0;

                if (resetShopComponent.isAvailable && (resetShopComponent.rollsCount - 1 >= _sceneData.shop.ResetShopData.FreeStartResetsCount))
                {
                    resetShopComponent.isAvailable = false;
                    _sceneData.shop.ResetButton.Button.interactable = false;
                }
            }
            _sceneData.shop.UpdateResetPrice(resetShopComponent.currentResetPrice);
            entity.Del<ResetShopUpdateComponent>();
        }
    }
}
