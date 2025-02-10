using Leopotam.Ecs;
using System.Collections.Generic;
public struct ShopBuyCommandsComponent
{
    public List<ShopBuyCommand> buyCommands;
}

public struct ShopBuyCommandsUpdateEventComponent
{
    public List<ShopBuyCommand> buyCommands;
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
// ������ ��������� ������, ���������
public struct PurchasedItemsComponent
{
    public List<ShopItemData> items;
}