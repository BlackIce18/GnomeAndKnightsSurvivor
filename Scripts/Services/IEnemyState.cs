using Leopotam.Ecs;
using UnityEngine;

public interface IEnemyState
{
    void Enter(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine);
    void Exit(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine);
    void Update(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine);
}
