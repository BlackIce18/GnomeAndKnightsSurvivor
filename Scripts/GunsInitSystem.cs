using Leopotam.Ecs;
using UnityEngine;

public class GunsInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private Guns _guns;
    private ActiveGuns _activeGuns;


    public void Init()
    {
        for(int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            _activeGuns.GunList[i] = InitGun(_activeGuns.GunList[i]);
        }
    }

    public ActiveGunComponent InitGun(ActiveGunComponent activeGunComponent)
    {
        int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
        activeGunComponent.pool = new ObjectPool<BulletComponent>(elementsCount);
        activeGunComponent.gun.attackInterval = activeGunComponent.gun.Data.gunData.attackRate;

        for (int j = 0; j < elementsCount; j++)
        {
            var gun = activeGunComponent.gun.Data;
            EcsEntity entity = _world.NewEntity();
            ref BulletComponent bullet = ref entity.Get<BulletComponent>();
            bullet.belongsToPool = activeGunComponent.pool;
            bullet.instance = GameObject.Instantiate(gun.gunData.prefab, _guns.parentForBullets);
            bullet.instance.SetActive(false);
            bullet.instance.GetComponent<Bullet>().entity = entity;
            bullet.damage = gun.bulletData.damage;
            bullet.maxLifeTime = gun.bulletData.lifeTime;
            bullet.speed = gun.bulletData.speed;
            bullet.size = gun.bulletData.size;
            activeGunComponent.pool.AddToPool(bullet);
        }
        return activeGunComponent;
    }
}
