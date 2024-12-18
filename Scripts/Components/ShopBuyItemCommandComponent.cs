using Leopotam.Ecs;
using System.Collections.Generic;
public struct ActiveShopItemsComponent
{
    public List<ShopItemGunData> shopItems;
}

public struct ActiveShopItemsUpdateEventComponent
{
    public List<ShopItemGunData> shopItems;
}
public struct ShopBuyItemCommandComponent
{
    public List<ICommand> list;
    public bool isAvailable;
    public EcsEntity shopEntity;
}

// ���� ������ ��������� �������
// ����� ��������� ���������
public struct ShopBuyItemEventComponent 
{
    public ShopItemData item;
}
public struct ShopBuyGunEventComponent
{
    public ShopItemGunData item;
}
// ������ ��������� ������, ���������
public struct PurchasedItemsComponent
{
    public List<ShopItemData> items;
}
