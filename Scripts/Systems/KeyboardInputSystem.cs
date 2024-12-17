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
    private ICommand s1;
    private ICommand s2;
    private ICommand s3;
    private ICommand s4;
    private ICommand r;

    private KeyCode _activateLShift = KeyCode.LeftShift;
    private bool _isAvailable = true;
    private bool _lShiftPressed = false;

    private EcsFilter<ShopBuyItemCommandComponent, ResetShopComponent> _filterShopBuyItemComponent = null;

    public void Init()
    {
        ref List<ShopBuyCommand> shopItemsList = ref _filterShopBuyItemComponent.Get1(0).list;
        s1 = shopItemsList[0];
        s2 = shopItemsList[1];
        s3 = shopItemsList[2];
        s4 = shopItemsList[3];
        alpha1 = shopItemsList[4];
        alpha2 = shopItemsList[5];
        alpha3 = shopItemsList[6];
        alpha4 = shopItemsList[7];
        ref ResetShopCommand resetShopCommand = ref _filterShopBuyItemComponent.Get2(0).resetShopCommand;
        r = resetShopCommand;
    }

    public void Run()
    {
        if(_isAvailable)
        {
            _lShiftPressed = Input.GetKey(_activateLShift) ? true : false;

            if (_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha1))
            {
                s1.Execute();
            }

            if (_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha2))
            {
                s2.Execute();
            }

            if (_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha3))
            {
                s3.Execute();
            }

            if (_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha4))
            {
                s4.Execute();
            }

            if (!_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha1))
            {
                alpha1.Execute();
            }

            if (!_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha2))
            {
                alpha2.Execute();
            }

            if (!_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha3))
            {
                alpha3.Execute();
            }

            if (!_lShiftPressed && Input.GetKeyDown(KeyCode.Alpha4))
            {
                alpha4.Execute();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                r.Execute();
            }
        }
    }
}