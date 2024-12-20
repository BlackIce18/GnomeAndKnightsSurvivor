using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct BorderColorComponent
{
    public ShopUIButton shopUIButton;
    public ShopItemRarity rarity;
}
public struct BorderUpdateColorEventComponent : IEcsIgnoreInFilter {}
public class BorderColorsSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<BorderColorComponent, BorderUpdateColorEventComponent> _filter;
    public void Run()
    {        
        foreach (var index in _filter)
        {
            ref BorderColorComponent borderUpdateColorEventComponent = ref _filter.Get1(index);

            for (int i = 0; i < _sceneData.shopBorderColorsData.BorderColors.Count; i++)
            {
                if (_sceneData.shopBorderColorsData.BorderColors[i].rarity == borderUpdateColorEventComponent.rarity)
                {
                    borderUpdateColorEventComponent.shopUIButton.Border.color = _sceneData.shopBorderColorsData.BorderColors[i].color;
                    _filter.GetEntity(index).Del<BorderColorComponent>();
                    _filter.GetEntity(index).Del<BorderUpdateColorEventComponent>();
                    break;
                }
            }

        }
    }
}
