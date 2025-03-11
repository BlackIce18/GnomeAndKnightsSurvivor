using Leopotam.Ecs;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void Enter(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        Debug.Log("В режиме ожидания");
    }

    public void Exit(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        Debug.Log("Покидает режим ожидания");
    }

    public void Update(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {

        if (entity.Has<FollowComponent>())
        {
            ref var followComponent = ref entity.Get<FollowComponent>();
            ref var attackComponent = ref entity.Get<EnemyAttackComponent>();
            float distance = Vector3.Distance(enemyObject.transform.position, followComponent.target.position);
            if (distance > attackComponent.viewRange)
            {
                Debug.Log($"👀 {enemyObject.name} заметил игрока! Начинаем погоню.");
                stateMachine.ChangeState<EnemyChaseState>(entity, enemyObject);
            }
        }
        else
        {
            // Если у врага нет FollowComponent, он проверяет зону атаки
            if (entity.Has<EnemyAttackComponent>())
            {
                ref var attackComponent = ref entity.Get<EnemyAttackComponent>();
                Collider[] hits = Physics.OverlapSphere(enemyObject.transform.position, attackComponent.range);

                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Player"))
                    {
                        Debug.Log($"🎯 {enemyObject.name} видит игрока! Начинаем атаку.");
                        stateMachine.ChangeState<EnemyAttackState>(entity, enemyObject);
                        return;
                    }
                }
            }
        }
    }
}
