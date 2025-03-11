using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSystem : IEcsInitSystem, IEcsRunSystem
{
    private readonly EcsFilter<EnemyStateComponent, EnemyComponent> _enemyFilter = null;
    private Dictionary<int, EnemyStateMachine> _stateMachines = new();

    public void Init()
    {
        foreach (var i in _enemyFilter)
        {
            ref var entity = ref _enemyFilter.GetEntity(i);
            ref var stateComponent = ref _enemyFilter.Get1(i);
            ref var enemyComponent = ref _enemyFilter.Get2(i);

            int entityId = entity.GetHashCode();
            _stateMachines[entityId] = new EnemyStateMachine();
            _stateMachines[entityId].ChangeState<EnemyIdleState>(entity, enemyComponent.instance);
        }
    }

    public void Run()
    {
        foreach (var i in _enemyFilter)
        {
            ref var entity = ref _enemyFilter.GetEntity(i);
            ref var enemyComponent = ref _enemyFilter.Get2(i);

            int entityId = entity.GetHashCode();
            if (_stateMachines.TryGetValue(entityId, out var stateMachine))
            {
                stateMachine.Update(entity, enemyComponent.instance);
            }
        }
    }
}
