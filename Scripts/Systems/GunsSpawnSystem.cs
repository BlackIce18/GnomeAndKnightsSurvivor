using Leopotam.Ecs;
using UnityEngine;

public class GunsSpawnSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private ActiveGuns _activeGuns;

    private AttackTypeInjector _attackTypeInjector;
    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter;
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItemFilter;
    public void Init()
    {
        _attackTypeInjector = new AttackTypeInjector(this);

        SpawnNewGun();
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
                bool isExist = false;
                for (int i = 0; i < _activeGuns.GunList.Count; i++)
                {
                    if (_activeGuns.GunList[i].gun.GunAndBulletData.gunData.shopItemData == shopBuyEvent.item)
                    {
                        ActiveGunComponent activeGunComponent = _activeGuns.guns[i];
                        activeGunComponent.count++;
                        _activeGuns.guns[i] = activeGunComponent;
                        isExist = true;
                        break;
                    }
                }

                if (isExist == false)
                {
                    ActiveGunComponent activeGunComponent = new ActiveGunComponent();
                    GameObject gunGameObject = GameObject.Instantiate(gunData.datas.gunData.gunPrefab, _activeGuns.transform);
                    activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
                    activeGunComponent.gun.attackInterval = gunData.datas.gunData.attackInterval;
                    activeGunComponent.count++;
                    //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

                    activeGunComponent.bulletPoolSizeForOneGun = 10;
                    int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
                    activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

                    for (int q = 0; q < elementsCount; q++)
                    {
                        BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                        activeGunComponent.bulletPool.AddToPool(bulletComponent);
                    }

                    purchasedItemsList.items.Add(shopBuyEvent.item);
                    _activeGuns.GunList.Add(activeGunComponent);
                }

                entity.Del<ShopBuyItemEventComponent>();
            }
        }
    }

    public void SpawnNewGun()
    {
        for (int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            ActiveGunComponent activeGunComponent = _activeGuns.GunList[i];
            activeGunComponent.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackInterval;

            int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
            activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

            for (int j = 0; j < elementsCount; j++)
            {
                BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                activeGunComponent.bulletPool.AddToPool(bulletComponent);
            }

            _activeGuns.GunList[i] = activeGunComponent;
        }
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

        bullet.attackType = _attackTypeInjector.Inject(activeGunComponent.gun.GunAndBulletData.gunData.attackType);

        return bullet;
    }
}
