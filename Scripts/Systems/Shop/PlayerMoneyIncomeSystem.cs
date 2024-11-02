using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneyIncomeSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<WalletComponent> _filter;
    private float _spawnTime;
    private float _elapsedTime = 0;
    public void Init()
    {
        _spawnTime = _sceneData.walletData.incomeInterval;
    }

    public void Run()
    {
        if ((_elapsedTime += Time.deltaTime) >= _spawnTime)
        {
            foreach(var walletComponent in _filter)
            {
                ref WalletComponent wallet = ref _filter.Get1(walletComponent);
                ref var entity = ref _filter.GetEntity(walletComponent);
                ref var walletUpdateComponent = ref entity.Get<WalletUpdateComponent>();
                wallet.money += wallet.moneyIncome;
                walletUpdateComponent.moneyIncome = wallet.moneyIncome;
                walletUpdateComponent.money = wallet.money;
            }

            _elapsedTime = 0;
        }
        //_sceneData.moneyText.text = wallet.money.ToString();
    }
}
