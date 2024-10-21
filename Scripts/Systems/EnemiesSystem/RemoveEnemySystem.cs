using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemySystem : IEcsRunSystem
{
    private EcsFilter<DefenceComponent, EnemyComponent> _filter = null;
    private EcsFilter<EnemiesPoolComponent> _enemiesPool = null;
    private EcsFilter<OnTriggerEnterComponent> _onTriggerEnter = null;

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
            }
        }
    }
}
