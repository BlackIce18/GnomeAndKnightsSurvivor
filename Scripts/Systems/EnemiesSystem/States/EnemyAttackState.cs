using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void Enter(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        Debug.Log("���� �������� �����");
    }

    public void Exit(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        Debug.Log("���� ���������� �����!");
        stateMachine.ChangeState<EnemyIdleState>(entity, enemyObject);
    }

    public void Update(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        if (entity.Has<FollowComponent>())
        {
            ref var followComponent = ref entity.Get<FollowComponent>();
            ref var attackComponent = ref entity.Get<EnemyAttackComponent>();

            if (followComponent.target == null)
            {
                Debug.LogError("������: FollowComponent.target == null!");
                return;
            }

            if (enemyObject == null)
            {
                Debug.LogError("������: enemyObject == null!");
                return;
            }
            float distance = Vector3.Distance(enemyObject.transform.position, followComponent.target.position);
            Debug.Log(distance);
            if (distance > attackComponent.range)
            {
                stateMachine.ChangeState<EnemyChaseState>(entity, enemyObject);
                Debug.Log("���� �������� �������������, �� ��������� �����");
            }
        }
    }
}
