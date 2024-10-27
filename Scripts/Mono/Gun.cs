using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GunAndBulletDatas data;
    [HideInInspector] public float attackInterval;

    public GunAndBulletDatas GunAndBulletData { get { return data; } }
}
