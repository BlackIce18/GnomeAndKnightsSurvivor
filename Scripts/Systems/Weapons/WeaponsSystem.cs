using Leopotam.Ecs;
using System;
using UnityEngine;

public interface IWeaponSystem
{
    public void InitGun(GunData gunData);
    public void Attack(GameObject target);
}
public struct SingleTargetWeaponBuyEvent : IEcsIgnoreInFilter { }
public class SingleBulletWeaponSystem : IWeaponSystem, IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter<MovableComponent, UserInputComponent> _filter;
    private ActiveGuns _activeGuns;
    private EcsFilter<ActiveBulletsComponent> _activeBulletsFilter;
    private EcsFilter<ActiveGunComponent> _activeGunFilter;
    private EcsFilter<ShopBuyItemEventComponent, SingleTargetWeaponBuyEvent> _shopBuyItemFilter;
    public void Run()
    {
        InitNewGunIfPurchased();

        ref ActiveBulletsComponent activeBullets = ref _activeBulletsFilter.Get1(0);
        ref var characterPosition = ref _filter.Get1(0);
        ref var userInput = ref _filter.Get2(0);

        foreach(var i in _activeGunFilter)
        {
            ref var activeGun = ref _activeGunFilter.Get1(i);

            if ((activeGun.gun.attackInterval -= Time.deltaTime) > 0)
            {
                continue;
            }

            activeGun.gun.attackInterval = activeGun.gun.GunAndBulletData.gunData.attackInterval;
            
            float positionY = activeGun.gun.GunAndBulletData.gunData.bulletPrefab.transform.position.y;
            Vector3 bulletStartPosition = new Vector3(characterPosition.transform.position.x, positionY, characterPosition.transform.position.z);
            Vector3 bulletTargetPosition = new Vector3(userInput.mousePositionAtTerrain.x, positionY, userInput.mousePositionAtTerrain.z - 1.5f);// -1 из-за смещения по y на 1, -1 за движение по z

            activeBullets.list.Add(SpawnBullet(bulletStartPosition, bulletTargetPosition, activeGun.bulletPool));
        }
    }

    private void InitNewGunIfPurchased() 
    {
        foreach (var index in _shopBuyItemFilter)
        {
            ref var entity = ref _shopBuyItemFilter.GetEntity(index);
            ref var shopBuyEvent = ref _shopBuyItemFilter.Get1(index);

            if (shopBuyEvent.item is ShopItemGunData gunData)
            {
                InitGun(gunData.datas.gunData);
            }

            entity.Del<ShopBuyItemEventComponent>();
            entity.Del<SingleTargetWeaponBuyEvent>();
        }
    }

    private BulletComponent SpawnBullet(Vector3 startBulletPosition, Vector3 endBulletPosition, ObjectPool<BulletComponent> pool)
    {
        BulletComponent bullet = pool.GetFromPool();

        bullet.startPosition = startBulletPosition;
        bullet.endPosition = endBulletPosition;

        var heading = bullet.endPosition - bullet.startPosition;
        var distance = heading.magnitude;
        bullet.direction = heading / distance;

        bullet.instance.SetActive(true);
        bullet.instance.transform.position = bullet.startPosition;

        return bullet;
    }

    public void InitGun(GunData gunData) // ShopItemGunData.datas.gunData
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
        GameObject gunGameObject = GameObject.Instantiate(gunData.gunPrefab, _activeGuns.transform);
        activeGunComponent.gun = gunGameObject.GetComponent<Gun>();
        activeGunComponent.gun.attackInterval = gunData.attackInterval;
        //activeGunComponent.gun.GunAndBulletData.gunData = purchasedItems.items[j].datas.gunData;

        // Выбор пула / создание пула
        int elementsCount = activeGunComponent.bulletPoolSizeForOneGun = 10;
        activeGunComponent.bulletPool = new ObjectPool<BulletComponent>(elementsCount);

        for (int q = 0; q < elementsCount; q++)
        {
            BulletComponent bulletComponent = CreateBullet(activeGunComponent);
            
            activeGunComponent.bulletPool.AddToPool(bulletComponent);
        }
    }
    private BulletComponent CreateBullet(ActiveGunComponent activeGunComponent)
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

        bullet.attackType = new AttackTypeInjector().Inject(activeGunComponent.gun.GunAndBulletData.gunData.attackType);

        return bullet;
    }
    public void Attack(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
