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

// Если купили создается событие
// После обработки удаляется
public struct ShopBuyItemEventComponent 
{
    public ShopItemData item;
}
public struct ShopBuyGunEventComponent
{
    public ShopItemGunData item;
}
// Список купленных оружий, улучшений
public struct PurchasedItemsComponent
{
    public List<ShopItemData> items;
}
