using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemySystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<DefenceComponent, EnemyComponent, OnTriggerEnterComponent> _filter = null;
    private EcsFilter<EnemiesPoolComponent> _enemiesPool = null;
    private EcsFilter<OnTriggerEnterComponent> _onTriggerEnter = null;
    private EcsFilter<WalletComponent> _walletComponent = null;

    private void UpdateWallet(ref WalletComponent walletComponent, int price)
    {
        ref var walletEntity = ref _walletComponent.GetEntity(0);
        ref WalletUpdateComponent walletUpdateComponent = ref walletEntity.Get<WalletUpdateComponent>();
        walletComponent.money += price;
        walletUpdateComponent.money = walletComponent.money;
        walletUpdateComponent.killBounty = walletComponent.killBounty;
        walletUpdateComponent.moneyIncome = walletComponent.moneyIncome;
    }

    public void Run()
    {
        foreach(var i in _filter)
        {
            ref DefenceComponent defenceComponent = ref _filter.Get1(i);
            ref EnemyComponent enemyComponent = ref _filter.Get2(i);

            if(defenceComponent.hp <= 0)
            {
                enemyComponent.instance.SetActive(false);
                enemyComponent.parentPool.AddToPool(enemyComponent);
                int killBounty = enemyComponent.enemyData.goldForKill * _sceneData.shop.CurrentKillBounty / 100;
                
                UpdateWallet(ref _walletComponent.Get1(0), killBounty);
            }

            _filter.GetEntity(i).Del<OnTriggerEnterComponent>();
        }
    }
}
