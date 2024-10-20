using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunAndBulletDatas data;
    [HideInInspector] public float attackInterval;

    public GunAndBulletDatas GunAndBulletData { get { return data; } }

    /*
    readonly ObjectPool<BulletComponent> _pool;
    public BulletComponent GetBullet()
    {
        if (_pool.PoolSize > 0)
        {
            BulletComponent bulletComponent = _pool.GetFromPool();
            // “”“ Õ≈ ƒŒÀ∆ÕŒ ¡€“‹ »Õ»“¿
            bulletComponent.speed = data.bulletData.speed;
            bulletComponent.size = data.bulletData.size;
            //bulletComponent.instance = GameObject.Instantiate(data.gunData.prefab, _guns.parentForBullets);
            return bulletComponent;
        }

        return new BulletComponent();
    }

    public void ReleaseBullet(BulletComponent bulletComponent)
    {
        bulletComponent.Default();
        _pool.AddToPool(bulletComponent);
    }
    */
}
