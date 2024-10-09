using System;
using UnityEngine;

[Serializable]
public struct ActiveGunComponent
{
    public Gun gun;
    [Min(1)] public int bulletPoolSizeForOneGun;
    [Min(1)] public int count;
    public ObjectPool<BulletComponent> pool;
    public GameObject target;
}
