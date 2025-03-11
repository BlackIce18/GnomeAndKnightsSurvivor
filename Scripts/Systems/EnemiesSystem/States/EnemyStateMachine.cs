using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyStateMachine
{
    private Dictionary<Type, IEnemyState> _states = new();
    private IEnemyState _currentState;

    public EnemyStateMachine()
    {
        _states[typeof(EnemyIdleState)] = new EnemyIdleState();
        _states[typeof(EnemyChaseState)] = new EnemyChaseState();
        _states[typeof(EnemyAttackState)] = new EnemyAttackState();
    }

    public void ChangeState<T>(EcsEntity entity, GameObject enemyObject) where T : IEnemyState
    {
        _currentState?.Exit(entity, enemyObject, this);
        _currentState = _states[typeof(T)];
        _currentState.Enter(entity, enemyObject, this);
    }

    public void Update(EcsEntity entity, GameObject enemyObject)
    {
        _currentState?.Update(entity, enemyObject, this);
    }
}