using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public int startHp;
    public float playerSpeed;
}
