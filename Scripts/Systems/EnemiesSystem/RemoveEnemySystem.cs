using Leopotam.Ecs;

public class RemoveEnemySystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<DefenceComponent, EnemyComponent, OnTriggerEnterComponent> _filter = null;
    private EcsFilter<WalletComponent> _walletFilter = null;
    private EcsEntity _walletEntity;
    private EcsFilter<LvlComponent, XpComponent> _lvlFilter = null;

    public void Init()
    {
        _walletEntity = _walletFilter.GetEntity(0);
    }

    public void Run()
    {
        foreach(var i in _filter)
        {
            ref DefenceComponent defenceComponent = ref _filter.Get1(i);
            ref EnemyComponent enemyComponent = ref _filter.Get2(i);

            if(defenceComponent.hp <= 0)
            {
                enemyComponent.instance.SetActive(false);
                enemyComponent.parentPool.AddToPool(enemyComponent);

                foreach(var index in _lvlFilter)
                {
                    ref var lvlEntity = ref _lvlFilter.GetEntity(index);
                    lvlEntity.Get<XpUpdateComponent>().xp = enemyComponent.enemyData.Xp;
                }

                _walletEntity.Get<KillBountyEventComponent>().killBounty = enemyComponent.enemyData.goldForKill;
            }

            _filter.GetEntity(i).Del<OnTriggerEnterComponent>();
        }
    }


}
