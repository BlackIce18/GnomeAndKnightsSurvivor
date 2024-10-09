using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BulletMoverSystem : IEcsRunSystem
{
    private EcsFilter<ActiveBulletsComponent> _activeBullets;

    public void Move()
    {
        foreach (var activeBullet in _activeBullets)
        {
            ref ActiveBulletsComponent activeBullets = ref _activeBullets.Get1(activeBullet);

            foreach (var i in activeBullets.list)
            {
                BulletComponent bullet = i;
                if (bullet.instance.activeSelf == true)
                {
                    Vector3 position = new Vector3(bullet.direction.x, 0, bullet.direction.z);
                    bullet.instance.transform.Translate(position * bullet.speed * Time.deltaTime);
                }
            }
        }
    }

    public void Run()
    {
        Move();
    }
}
