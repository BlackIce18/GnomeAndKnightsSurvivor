using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIncomeSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<WalletComponent> _walletComponent;
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItemFilter;
    private float _spawnTime;
    private float _elapsedTime = 0;
    public void Init()
    {
        _spawnTime = _sceneData.walletData.incomeInterval;
    }

    public void Run()
    {
        foreach (var index in _shopBuyItemFilter)
        {
            ref var shopBuyEvent = ref _shopBuyItemFilter.Get1(index);
            ref var entity = ref _shopBuyItemFilter.GetEntity(index);

            if (shopBuyEvent.item is ShopItemIncomeData incomeData)
            {
                foreach (var walletComponent in _walletComponent)
                {
                    ref WalletComponent wallet = ref _walletComponent.Get1(walletComponent);
                    ref var walletEntity = ref _walletComponent.GetEntity(walletComponent);
                    ref var walletUpdateComponent = ref walletEntity.Get<WalletUpdateComponent>();
                    walletUpdateComponent.moneyIncome = wallet.moneyIncome + incomeData.additionalIncome;
                    walletUpdateComponent.money = wallet.money;
                    walletUpdateComponent.killBounty = wallet.killBounty;
                }

                entity.Del<ShopBuyItemEventComponent>();
            }
        }

        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            foreach(var walletComponent in _walletComponent)
            {
                ref WalletComponent wallet = ref _walletComponent.Get1(walletComponent);
                ref var entity = ref _walletComponent.GetEntity(walletComponent);
                ref var walletUpdateComponent = ref entity.Get<WalletUpdateComponent>();
                wallet.money += wallet.moneyIncome;
                walletUpdateComponent.moneyIncome = wallet.moneyIncome;
                walletUpdateComponent.money = wallet.money;
                walletUpdateComponent.killBounty = wallet.killBounty;
            }

            _elapsedTime = 0;
        }

            //_sceneData.moneyText.text = wallet.money.ToString();
    }
}
