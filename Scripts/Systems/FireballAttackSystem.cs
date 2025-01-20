using Leopotam.Ecs;
using UnityEngine;

public class FireballAttackSystem : IEcsRunSystem
{
    private EcsFilter<MovableComponent, UserInputComponent> _filter;
    private ActiveGuns _activeGuns;
    private EcsFilter<ActiveBulletsComponent> _activeBulletsFilter;
    private EcsFilter<ActiveGunComponent> _activeGunFilter;
    public void Run()
    {
        ref ActiveBulletsComponent activeBullets = ref _activeBulletsFilter.Get1(0);
        ref var characterPosition = ref _filter.Get1(0);
        ref var userInput = ref _filter.Get2(0);

        foreach(var i in _activeGunFilter)
        {
            ref var activeGun = ref _activeGunFilter.Get1(i);

            //ActiveGunItem activeGunComponent = _activeGuns.GunList[i];
            if ((activeGun.gun.attackInterval -= Time.deltaTime) > 0)
            {
                continue;
            }
            float positionY = activeGun.gun.GunAndBulletData.gunData.bulletPrefab.transform.position.y;
            Vector3 bulletStartPosition = new Vector3(characterPosition.transform.position.x, positionY, characterPosition.transform.position.z);
            Vector3 bulletEndPosition = new Vector3(userInput.mousePositionAtTerrain.x, positionY, userInput.mousePositionAtTerrain.z - 1.5f);// -1 из-за смещения по y на 1, -1 за движение по z

            activeGun.gun.attackInterval = activeGun.gun.GunAndBulletData.gunData.attackInterval;
            activeBullets.list.Add(Spawn(bulletStartPosition, bulletEndPosition, activeGun.bulletPool));

            /*for (int j = 0; j < activeGun.count; j++)
            {


                activeGun.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackInterval;

                activeBullets.list.Add(Spawn(bulletStartPosition, bulletEndPosition, activeGun.bulletPool));
            }*/
        }
        /*
        for (int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            ActiveGunItem activeGunComponent = _activeGuns.GunList[i];
            float positionY = activeGunComponent.gun.GunAndBulletData.gunData.bulletPrefab.transform.position.y;
            Vector3 bulletStartPosition = new Vector3(characterPosition.transform.position.x, positionY, characterPosition.transform.position.z);
            Vector3 bulletEndPosition = new Vector3(userInput.mousePositionAtTerrain.x, positionY, userInput.mousePositionAtTerrain.z - 1.5f);// -1 из-за смещения по y на 1, -1 за движение по z

            for (int j = 0; j < activeGunComponent.count; j++)
            {
                if ((activeGunComponent.gun.attackInterval -= Time.deltaTime) > 0)
                {
                    _activeGuns.GunList[i] = activeGunComponent;
                    continue;
                }

                activeGunComponent.gun.attackInterval = activeGunComponent.gun.GunAndBulletData.gunData.attackInterval;

                activeBullets.list.Add(Spawn(bulletStartPosition, bulletEndPosition, _activeGuns.GunList[i].bulletPool));
            }
        }*/
    }

    private BulletComponent Spawn(Vector3 startBulletPosition, Vector3 endBulletPosition, ObjectPool<BulletComponent> pool)
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
}
