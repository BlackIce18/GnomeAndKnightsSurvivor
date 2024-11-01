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
            ref WalletComponent wallet = ref _filter.Get1(0);
            wallet.money += wallet.moneyIncome;
            _sceneData.moneyText.text = wallet.money.ToString();

            _elapsedTime = 0;
        }
    }
}
