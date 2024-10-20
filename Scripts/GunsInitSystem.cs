using Leopotam.Ecs;
using UnityEngine;

public class GunsInitSystem : IEcsInitSystem
{
    private EcsWorld _world;
    private SceneData _sceneData;
    private ActiveGuns _activeGuns;

    private IAttackType _attackType;
    public void Inject(IAttackType attackType)
    {
        //_attackType = attackType;
    }
    public void Init()
    {
        for(int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            _activeGuns.GunList[i] = InitGun(_activeGuns.GunList[i]);
        }
    }

    public IAttackType Inject(AttackTypeData attackTypeData)
    {
        IAttackType attackType;

        switch (attackTypeData.attackType)
        {
            case AttackTypeEnum.Normal:
                attackType = new NormalAttack(attackTypeData);
                break;
            case AttackTypeEnum.Piercing:
                attackType = new PiercingAttack(attackTypeData);
                break;
            default:
                attackType = new NormalAttack(attackTypeData);
                break;
        }

        return attackType;
    }

    public ActiveGunComponent InitGun(ActiveGunComponent activeGunComponent)
    {
        activeGunComponent.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackRate;

        int elementsCount = activeGunComponent.bulletPoolSizeForOneGun * activeGunComponent.count;
        activeGunComponent.pool = new ObjectPool<BulletComponent>(elementsCount);

        for (int j = 0; j < elementsCount; j++)
        {
            activeGunComponent.pool.AddToPool(CreateNewBullet(activeGunComponent));
        }

        return activeGunComponent;
    }

    private BulletComponent CreateNewBullet(ActiveGunComponent activeGunComponent)
    {
        EcsEntity entity = _world.NewEntity();
        ref BulletComponent bullet = ref entity.Get<BulletComponent>();
        bullet.belongsToPool = activeGunComponent.pool;
        bullet.instance = GameObject.Instantiate(activeGunComponent.gun.GunAndBulletData.gunData.prefab, _sceneData.parentForBullets);
        bullet.instance.SetActive(false);
        bullet.instance.GetComponent<Bullet>().entity = entity;
        bullet.damage = activeGunComponent.gun.GunAndBulletData.bulletData.damage;
        bullet.maxLifeTime = activeGunComponent.gun.GunAndBulletData.bulletData.lifeTime;
        bullet.speed = activeGunComponent.gun.GunAndBulletData.bulletData.speed;
        bullet.size = activeGunComponent.gun.GunAndBulletData.bulletData.size;

        bullet.attackType = Inject(activeGunComponent.gun.GunAndBulletData.gunData.attackType);

        //bullet.attackType = Inject(activeGunComponent.gun.GunAndBulletData.gunData.attackType, );

        return bullet;
    }
}
