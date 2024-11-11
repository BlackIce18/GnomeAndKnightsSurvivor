using Leopotam.Ecs;
using System.Collections.Generic;

public struct ShopBuyItemCommandComponent
{
    public List<ShopBuyItemCommand> list;
    public bool isAvailable;
    public EcsEntity shopEntity;
}

public struct ShopBuyItemEventComponent 
{
    public ShopItemData item;
}

public struct PurchasedItemsComponent
{
    public List<ShopItemData> items;
}