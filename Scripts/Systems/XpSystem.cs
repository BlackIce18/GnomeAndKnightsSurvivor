using Leopotam.Ecs;

public class XpSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<XpComponent, XpUpdateComponent, LvlComponent> _xpUpdateFilter;
    public void Run()
    {
        foreach(var i in _xpUpdateFilter)
        {
            ref var xpComponent = ref _xpUpdateFilter.Get1(i);
            ref var xpUpdateComponent = ref _xpUpdateFilter.Get2(i);
            ref var lvlComponent = ref _xpUpdateFilter.Get3(i);
            ref var entity = ref _xpUpdateFilter.GetEntity(i);

            int result = xpComponent.current + xpUpdateComponent.xp;

            if (result >= xpComponent.max)
            {
                result -= xpComponent.max;

                entity.Get<LvlUpUpdateComponent>();
            }

            _sceneData.xpSlider.value = result;
            _sceneData.xpSlider.maxValue = _sceneData.lvlAndXpData.xpToReachLevel[lvlComponent.current - 1];
            xpComponent.current = result;

            entity.Del<XpUpdateComponent>();
        }
    }
}
