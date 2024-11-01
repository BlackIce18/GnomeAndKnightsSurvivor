using Leopotam.Ecs;
using UnityEngine;

public class ShopBuyItemCommand : IShopButtonCommand
{
    public void BuyItem()
    {
        Debug.Log("123");
    }
}

public class InputControllerSystem : IEcsRunSystem, IEcsInitSystem
{
    IShopButtonCommand alpha1;
    IShopButtonCommand alpha2;
    IShopButtonCommand alpha3;
    IShopButtonCommand alpha4;
    IShopButtonCommand f1;
    IShopButtonCommand f2;
    IShopButtonCommand f3;
    IShopButtonCommand f4;

    public void Init()
    {
        alpha1 = new ShopBuyItemCommand();
        alpha2 = new ShopBuyItemCommand();
        alpha3 = new ShopBuyItemCommand();
        alpha4 = new ShopBuyItemCommand();
        f1 = new ShopBuyItemCommand();
        f2 = new ShopBuyItemCommand();
        f3 = new ShopBuyItemCommand();
        f4 = new ShopBuyItemCommand();
    }

    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            alpha1.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            alpha2.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            alpha3.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            alpha4.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            f1.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            f2.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            f3.BuyItem();
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            f4.BuyItem();
        }
    }
}
