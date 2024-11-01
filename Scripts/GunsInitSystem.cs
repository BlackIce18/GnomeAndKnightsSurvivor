using Leopotam.Ecs;
using UnityEngine;

public class GunsInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private ActiveGuns _activeGuns;

    private AttackTypeInjector _attackTypeInjector;

    public void Init()
    {
        _attackTypeInjector = new AttackTypeInjector(this);

        for(int i = 0; i < _activeGuns.GunList.Count; i++)
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
        }
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
