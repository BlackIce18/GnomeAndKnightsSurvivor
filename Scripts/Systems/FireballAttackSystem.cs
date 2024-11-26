using Leopotam.Ecs;
using UnityEngine;

public class FireballAttackSystem : IEcsRunSystem
{
    private EcsFilter<MovableComponent, UserInputComponent> _filter;
    private ActiveGuns _activeGuns;
    private EcsFilter<ActiveBulletsComponent> _activeBullets;

    public void Run()
    {
        ref ActiveBulletsComponent activeBullets = ref _activeBullets.Get1(0);
        ref var characterPosition = ref _filter.Get1(0);
        Vector3 bulletStartPosition = characterPosition.transform.position;
        ref var input = ref _filter.Get2(0);

        for (int i = 0; i < _activeGuns.GunList.Count; i++)
        {
            ActiveGunComponent activeGunComponent = _activeGuns.GunList[i];
            float positionY = activeGunComponent.gun.GunAndBulletData.gunData.bulletPrefab.transform.position.y;
            Vector3 bulletEndPosition = new Vector3(input.mousePositionAtTerrain.x, positionY, input.mousePositionAtTerrain.z - 1.5f);// -1 из-за смещения по y на 1, -1 за движение по z

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
        }
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
