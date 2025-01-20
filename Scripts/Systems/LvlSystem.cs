using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<XpComponent, LvlComponent, LvlUpUpdateComponent> _lvlFilter;

    public void Run()
    {
        foreach (var i in _lvlFilter)
        {
            ref var xpComponent = ref _lvlFilter.Get1(i);
            ref var lvlComponent = ref _lvlFilter.Get2(i);
            ref var entity = ref _lvlFilter.GetEntity(i);

            lvlComponent.current++;

            entity.Del<LvlUpUpdateComponent>();
        }
    }
}
