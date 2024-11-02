using System.Collections.Generic;

public struct ActiveShopItemsComponent
{
    public List<ShopItemData> shopItems;
}

public struct ActiveShopItemsUpdateEventComponent
{
    public List<ShopItemData> shopItems;
    public ShopItemData updatedShopItem;
}