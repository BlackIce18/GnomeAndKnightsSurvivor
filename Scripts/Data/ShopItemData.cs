using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop/ShopItem", order = 1)]
public class ShopItemData : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
    public GunAndBulletDatas datas;
    public int cost;
}
