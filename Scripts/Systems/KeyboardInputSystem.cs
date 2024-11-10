using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class KeyboardInputSystem : IEcsRunSystem, IEcsInitSystem
{
    private ICommand alpha1;
    private ICommand alpha2;
    private ICommand alpha3;
    private ICommand alpha4;
    private ICommand f1;
    private ICommand f2;
    private ICommand f3;
    private ICommand f4;
    private ICommand r;
    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;

    public void Init()
    {
        ref List<ShopBuyItemCommand> shopItemsList = ref _filterShopBuyItemComponent.Get1(0).list;
        f1 = shopItemsList[0];
        f2 = shopItemsList[1];
        f3 = shopItemsList[2];
        f4 = shopItemsList[3];
        alpha1 = shopItemsList[4];
        alpha2 = shopItemsList[5];
        alpha3 = shopItemsList[6];
        alpha4 = shopItemsList[7];
        ref ResetShopCommand resetShopCommand = ref _filterShopBuyItemComponent.Get2(0).resetShopCommand;
        r = resetShopCommand;
    }

    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            alpha1.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            alpha2.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            alpha3.Execute();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            alpha4.Execute();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            f1.Execute();
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            f2.Execute();
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            f3.Execute();
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            f4.Execute();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            r.Execute();
        }
    }
}