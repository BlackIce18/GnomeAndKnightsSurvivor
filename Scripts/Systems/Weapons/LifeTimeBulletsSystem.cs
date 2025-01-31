using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimeBulletsSystem : IEcsRunSystem
{
    private EcsFilter<ActiveBulletsComponent> _activeBullets;

    public void Run()
    {
        ref ActiveBulletsComponent activeBullets = ref _activeBullets.Get1(0);
        for (int i = 0; i < activeBullets.list.Count; i++)
        {
            BulletComponent bullet = activeBullets.list[i];
            bullet.currentLifeTime += Time.deltaTime;
            activeBullets.list[i] = bullet;

            if (bullet.currentLifeTime >= bullet.maxLifeTime)
            {
                Destroy(bullet);
                activeBullets.list.RemoveAt(i);
            }
        }
    }

    private void Destroy(BulletComponent bulletComponent)
    {
        bulletComponent.Default();
        bulletComponent.belongsToPool.AddToPool(bulletComponent);
    }
}
