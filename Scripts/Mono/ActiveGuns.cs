using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct ActiveGunComponent
{
    public Gun gun;
    public int bulletPoolSizeForOneGun;
    public ObjectPool<BulletComponent> bulletPool;
    public GameObject target;
}

[Serializable]
public class ActiveGunItem
{
    public Gun gun;
    [Min(1)] public int bulletPoolSizeForOneGun;
    [Min(1)] public int count;
    public ObjectPool<BulletComponent> bulletPool;
    public GameObject target; 
}

public class ActiveGuns : MonoBehaviour
{
    //public List<ActiveGunItem> guns = new List<ActiveGunItem>();
    public Transform parentForBullets;

    //public List<ActiveGunItem> GunList { get { return guns; } set { guns = value; } }
}
