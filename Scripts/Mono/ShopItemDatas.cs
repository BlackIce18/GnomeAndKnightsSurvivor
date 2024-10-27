using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemDatas : MonoBehaviour
{
    [SerializeField] private List<ShopItemData> datas = new List<ShopItemData>();

    public List<ShopItemData> Datas { get { return datas; } }
}
