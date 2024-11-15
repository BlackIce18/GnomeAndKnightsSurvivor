using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ActiveGuns : MonoBehaviour
{
    public List<ActiveGunComponent> guns = new List<ActiveGunComponent>();
    public Transform parentForBullets;

    public List<ActiveGunComponent> GunList { get { return guns; } set { guns = value; } }
}
