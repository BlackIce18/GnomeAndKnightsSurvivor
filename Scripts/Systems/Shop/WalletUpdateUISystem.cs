using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletUpdateUISystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<WalletComponent, WalletUpdateComponent> _walletFilter;
    public void Run()
    {
        foreach(var walletComponent in _walletFilter)
        {
            ref WalletComponent wallet = ref _walletFilter.Get1(walletComponent);
            ref WalletUpdateComponent walletUpdate = ref _walletFilter.Get2(walletComponent);
            ref var entity = ref _walletFilter.GetEntity(walletComponent);
            _sceneData.moneyText.text = walletUpdate.money.ToString();
            _sceneData.moneyIncomeText.text = walletUpdate.moneyIncome.ToString();
            _sceneData.killBountyText.text = walletUpdate.killBounty.ToString() + "%";

            wallet.money = walletUpdate.money;
            wallet.moneyIncome = walletUpdate.moneyIncome;
            wallet.killBounty = walletUpdate.killBounty;
            entity.Del<WalletUpdateComponent>();
        }
    }
}
