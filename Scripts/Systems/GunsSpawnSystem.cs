using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

public class GunsSpawnSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private ActiveGuns _activeGuns;

    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter;
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItemFilter;
    private EcsFilter<ActiveGunComponent> _activeGunComponentFilter;
    public void Init()
    {
        //SpawnNewGun();
    }

    public void Run()
    {
        foreach (var index in _shopBuyItemFilter)
        {
            ref var shopBuyEvent = ref _shopBuyItemFilter.Get1(index);
            ref var entity = ref _shopBuyItemFilter.GetEntity(index);
            ref var purchasedItemsList = ref _purchasedItemsFilter.Get1(0);

            if(shopBuyEvent.item is ShopItemGunData gunData)
            {
                InitNewGun(gunData, purchasedItemsList, shopBuyEvent);
                /*if (IsGunExist(gunData, purchasedItemsList, shopBuyEvent) == false)
                {
                    InitNewGun(gunData, purchasedItemsList, shopBuyEvent);
                }*/

                entity.Del<ShopBuyItemEventComponent>();
            }
        }
    }
    private bool InitNewGun(ShopItemGunData gunData, PurchasedItemsComponent purchasedItemsList, ShopBuyItemEventComponent shopBuyEvent)
    {
        // Если оружие уже существует
        // Необходим для объединения нескольких пулов
        /*foreach (var i in _activeGunComponentFilter)
        {
            ref var activeGun = ref _activeGunComponentFilter.Get1(i);
            if (activeGun.gun.GunAndBulletData.gunData.shopItemData == shopBuyEvent.item)
            {

                Debug.Log("Gun Exist");
                return true;
            }
        }*/


        ref var activeGunComponent = ref _world.NewEntity().Get<ActiveGunComponent>();
        GameObject gunGameObject = GameObject.Instantiate(gunData.datas.gunData.gunPrefab, _activeGuns.transform);
        activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
        activeGunComponent.gun.attackInterval = gunData.datas.gunData.attackInterval;
        //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

        // Выбор пула / создание пула
        int elementsCount = activeGunComponent.bulletPoolSizeForOneGun = 10;
        activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);



        for (int q = 0; q < elementsCount; q++)
        {
            BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
            activeGunComponent.bulletPool.AddToPool(bulletComponent);
        }

        purchasedItemsList.items.Add(shopBuyEvent.item);

        return false;
    }
    private void InitNewGuns(ShopItemGunData gunData, PurchasedItemsComponent purchasedItemsList, ShopBuyItemEventComponent shopBuyEvent)
    {
        var entity = _world.NewEntity();
        ref var activeGunComponent = ref entity.Get<ActiveGunComponent>();
        GameObject gunGameObject = GameObject.Instantiate(gunData.datas.gunData.gunPrefab, _activeGuns.transform);
        activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
        activeGunComponent.gun.attackInterval = gunData.datas.gunData.attackInterval;
        //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

        // Выбор пула / создание пула
        int elementsCount = activeGunComponent.bulletPoolSizeForOneGun = 10;
        activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);



        for (int q = 0; q < elementsCount; q++)
        {
            BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
            activeGunComponent.bulletPool.AddToPool(bulletComponent);
        }

        purchasedItemsList.items.Add(shopBuyEvent.item);
        //_activeGuns.GunList.Add(activeGunComponent);
    }

    public void SpawnNewGun()
    {
        foreach(var i in _activeGunComponentFilter)
        {
            ref var activeGun = ref _activeGunComponentFilter.Get1(i);
            activeGun.gun.attackInterval = activeGun.gun.GunAndBulletData.gunData.attackInterval;

            int elementsCount = activeGun.bulletPoolSizeForOneGun;
            activeGun.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

            for (int j = 0; j < elementsCount; j++)
            {
                BulletComponent bulletComponent = CreateNewBullet(activeGun);
                activeGun.bulletPool.AddToPool(bulletComponent);
            }
        }

        //for (int i = 0; i < _activeGuns.GunList.Count; i++)
        //{
        //    ActiveGunItem activeGunComponent = _activeGuns.GunList[i];
        //    activeGunComponent.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackInterval;

        //    int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
        //    activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

        //    for (int j = 0; j < elementsCount; j++)
        //    {
        //        BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
        //        activeGunComponent.bulletPool.AddToPool(bulletComponent);
        //    }

        //    _activeGuns.GunList[i] = activeGunComponent;
        //}
    }

    

    private BulletComponent CreateNewBullet(ActiveGunComponent activeGunComponent)
    {
        EcsEntity entity = _world.NewEntity();
        ref BulletComponent bullet = ref entity.Get<BulletComponent>();
        bullet.belongsToPool = activeGunComponent.bulletPool;
        bullet.instance = GameObject.Instantiate(activeGunComponent.gun.GunAndBulletData.gunData.bulletPrefab, _activeGuns.parentForBullets);
        bullet.instance.SetActive(false);
        bullet.instance.GetComponent<Bullet>().entity = entity;
        bullet.damage = activeGunComponent.gun.GunAndBulletData.bulletData.damage;
        bullet.maxLifeTime = activeGunComponent.gun.GunAndBulletData.bulletData.lifeTime;
        bullet.speed = activeGunComponent.gun.GunAndBulletData.bulletData.speed;
        bullet.size = activeGunComponent.gun.GunAndBulletData.bulletData.size;

        bullet.attackType = new AttackTypeInjector(this).Inject(activeGunComponent.gun.GunAndBulletData.gunData.attackType);

        return bullet;
    }
}
