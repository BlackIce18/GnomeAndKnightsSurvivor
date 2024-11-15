using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawnerSystem : IEcsInitSystem, IEcsRunSystem
{
    private ActiveGuns _activeGuns;
    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter;

    public void Init()
    {
        Debug.Log(_activeGuns);
    }

    //private EcsFilter<PurchasedItemsComponent, PurchaseItemEventComponent> _purchaseItemEventFilter;
    public void Run()
    {
        foreach(var index in _purchasedItemsFilter)
        {
            ref var purchasedItems = ref _purchasedItemsFilter.Get1(index);
            ref var entity = ref _purchasedItemsFilter.GetEntity(index);

            for(int i = 0; i < purchasedItems.items.Count; i++)
            {

                ActiveGunComponent activeGunComponent = new ActiveGunComponent();

                _activeGuns.GunList.Add(activeGunComponent);
            }

            entity.Del<PurchasedItemsComponent>();
        }
    }
}
