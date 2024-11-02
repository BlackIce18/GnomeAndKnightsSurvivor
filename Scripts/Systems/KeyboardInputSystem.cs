using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class KeyboardInputSystem : IEcsRunSystem, IEcsInitSystem
{
    ICommand alpha1;
    ICommand alpha2;
    ICommand alpha3;
    ICommand alpha4;
    ICommand f1;
    ICommand f2;
    ICommand f3;
    ICommand f4;
    ICommand r;
    private EcsFilter<ShopBuyItemComponent> _shopBuyItemComponent = null;
    EcsFilter<KeyPressedEventComponent> _keysFilter = null;

    public void Init()
    {
        _keysFilter.Get1(0).keysPressed[KeyCode.Alpha1] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.Alpha2] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.Alpha3] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.Alpha4] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.F1] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.F2] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.F3] = false;
        _keysFilter.Get1(0).keysPressed[KeyCode.F4] = false;

        ref List<ShopBuyItemCommand> shopItemsList = ref _shopBuyItemComponent.Get1(0).list;
        alpha1 = shopItemsList[0];
        alpha2 = shopItemsList[1];
        alpha3 = shopItemsList[2];
        alpha4 = shopItemsList[3];
        f1 = shopItemsList[4];
        f2 = shopItemsList[5];
        f3 = shopItemsList[6];
        f4 = shopItemsList[7];
    }

    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha1] = true;
            alpha1.Execute();
        }
        else if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha1] = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha2] = true;
            alpha2.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha2] = false;
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha3] = true;
            alpha3.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha3] = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha4] = true;
            alpha4.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.Alpha4] = false;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F1] = true;
            f1.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F1] = false;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F2] = true;
            f2.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.F2))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F2] = false;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F3] = true;
            f3.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.F3))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F3] = false;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F4] = true;
            f4.Execute();
        }
        else if (Input.GetKeyUp(KeyCode.F4))
        {
            _keysFilter.Get1(0).keysPressed[KeyCode.F4] = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            r.Execute();
        }
    }
}