using Leopotam.Ecs;

public class GunsSpawnSystem : IEcsRunSystem
{
    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter;
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItemFilter;
    private EcsFilter<ActiveGunComponent> _activeGunComponentFilter;

    private WeaponFactory _weaponFactory = new WeaponFactory();

    public void Run()
    {
        foreach (var index in _shopBuyItemFilter)
        {
            ref var shopBuyEvent = ref _shopBuyItemFilter.Get1(index);
            ref var entity = ref _shopBuyItemFilter.GetEntity(index);
            ref var purchasedItemsList = ref _purchasedItemsFilter.Get1(0);

            if(shopBuyEvent.item is ShopItemGunData gunData)
            {
                //InitNewGun(gunData.datas);
                _weaponFactory.CreateWeapon(gunData.datas.gunData.gunId, entity);
                purchasedItemsList.items.Add(shopBuyEvent.item);

                
            }
             
        }
    }
}
