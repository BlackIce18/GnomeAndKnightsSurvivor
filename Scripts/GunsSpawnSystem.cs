using Leopotam.Ecs;
using UnityEngine;

public class GunsSpawnSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private ActiveGuns _activeGuns;

    private AttackTypeInjector _attackTypeInjector;
    private EcsFilter<PurchasedItemsComponent> _purchasedItemsFilter;
    private EcsFilter<ShopBuyItemEventComponent> _shopBuyItem;
    public void Init()
    {
        _attackTypeInjector = new AttackTypeInjector(this);

        SpawnNewGun();
    }

    public void Run()
    {
        foreach (var index in _shopBuyItem)
        {
            ref var purchasedItem = ref _shopBuyItem.Get1(index);
            ref var entity = ref _shopBuyItem.GetEntity(index);
            ref var purchasedItemsList = ref _purchasedItemsFilter.Get1(0);

            bool isExist = false;
            for (int i = 0; i < _activeGuns.GunList.Count; i++)
            {
                if (_activeGuns.GunList[i].gun.GunAndBulletData.gunData.shopItemData == purchasedItem.item)
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
                GameObject gunGameObject = GameObject.Instantiate(_sceneData.gunsPrefabList.prefabs[0]);
                activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
                activeGunComponent.gun.attackInterval = purchasedItem.item.datas.gunData.attackInterval;
                //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

                int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
                activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

                for (int q = 0; q < elementsCount; q++)
                {
                    BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                    activeGunComponent.bulletPool.AddToPool(bulletComponent);
                }

                purchasedItemsList.items.Add(purchasedItem.item);
                _activeGuns.GunList.Add(activeGunComponent);
            }

            entity.Del<ShopBuyItemEventComponent>();
        }
        /*
        foreach (var index in _purchasedItemsFilter)
        {
            ref var purchasedItems = ref _purchasedItemsFilter.Get1(index);
            ref var entity = ref _purchasedItemsFilter.GetEntity(index);

            /*
            Проходим по всем уже купленным оружиям
            Если нашли идентичный, тогда activeGunComponent.count++
            Если не нашли, тогда создать новое оружие
            Создать оружие
            Инициализировать данными поулченными из purchasedItems
            Добавить в ActiveGuns.List
             *//*
            for (int i = 0; i < _activeGuns.GunList.Count; i++) 
            {
                bool isExist = false;
                for(int j = 0; j < purchasedItems.items.Count; j++)
                {
                    if (_activeGuns.GunList[i].gun.GunAndBulletData.gunData.shopItemData == purchasedItems.items[j])
                    {
                        ActiveGunComponent activeGunComponent = _activeGuns.guns[i];
                        activeGunComponent.count++;
                        _activeGuns.guns[i] = activeGunComponent;
                        isExist = true;
                        break;
                    }
                }
                if(isExist == false)
                {
                    ActiveGunComponent activeGunComponent = new ActiveGunComponent();
                    GameObject gunGameObject = GameObject.Instantiate(_sceneData.gunsPrefabList.prefabs[0]);
                    activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
                    activeGunComponent.gun.attackInterval = purchasedItems.items[0].datas.gunData.attackInterval;
                    //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

                    int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
                    activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

                    for (int q = 0; q < elementsCount; q++)
                    {
                        BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                        activeGunComponent.bulletPool.AddToPool(bulletComponent);
                    }

                    _activeGuns.GunList.Add(activeGunComponent);
                }
            }
        */

            /*for (int i = 0; i < purchasedItems.items.Count; i++)
            {
                ActiveGunComponent activeGunComponent = new ActiveGunComponent();
                activeGunComponent.gun.attackInterval = purchasedItems.items[i].datas.gunData.attackInterval;
                //activeGunComponent.bulletPoolSizeForOneGun = purchasedItems.items[i].datas

                int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
                activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

                for (int j = 0; j < elementsCount; j++)
                {
                    BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                    activeGunComponent.bulletPool.AddToPool(bulletComponent);
                }

                _activeGuns.GunList.Add(activeGunComponent);
            }*/

            //entity.Del<PurchasedItemsComponent>();
        //}
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
        /*
        for (int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            ActiveGunComponent activeGunComponent = _activeGuns.GunList[i];
            activeGunComponent.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackRate;

            int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
            activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

            for (int j = 0; j < elementsCount; j++)
            {
                BulletComponent bulletComponent = CreateNewBullet(activeGunComponent);
                activeGunComponent.bulletPool.AddToPool(bulletComponent);
            }

            _activeGuns.GunList[i] = activeGunComponent;
        }*/
    }
    private BulletComponent CreateNewBullet(ActiveGunComponent activeGunComponent)
    {
        EcsEntity entity = _world.NewEntity();
        ref BulletComponent bullet = ref entity.Get<BulletComponent>();
        bullet.belongsToPool = activeGunComponent.bulletPool;
        bullet.instance = GameObject.Instantiate(activeGunComponent.gun.GunAndBulletData.gunData.prefab, _sceneData.parentForBullets);
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
