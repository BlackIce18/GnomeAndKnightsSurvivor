using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private readonly Dictionary<string, Action<EcsEntity>> _weaponCreators = new()
    {
        { "fireball", (entity) => {
            entity.Get<SingleTargetWeaponBuyEvent>(); // ��������� �������
        }},
        { "icegun", (entity) => {
            entity.Get<SingleTargetWeaponBuyEvent>(); // ��������� �������
        }},
        //{ "MultiTarget", () => new MultiTargetWeaponSystem() }
        // ��������� ������ ���� ������ �� ���� �������������
    };

    public void CreateWeapon(string gunId, EcsEntity entity)
    {
        if(_weaponCreators.TryGetValue(gunId, out var action))
        { 
            action(entity);
        }
    }
}
