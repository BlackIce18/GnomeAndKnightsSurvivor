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
            entity.Get<SingleTargetWeaponBuyEvent>(); // Добавляем событие
        }},
        { "icegun", (entity) => {
            entity.Get<SingleTargetWeaponBuyEvent>(); // Добавляем событие
        }},
        //{ "MultiTarget", () => new MultiTargetWeaponSystem() }
        // Добавляем другие типы оружия по мере необходимости
    };

    public void CreateWeapon(string gunId, EcsEntity entity)
    {
        if(_weaponCreators.TryGetValue(gunId, out var action))
        { 
            action(entity);
        }
    }
}
