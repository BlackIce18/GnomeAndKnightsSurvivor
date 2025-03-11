using Leopotam.Ecs;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void Enter(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        entity.Get<FollowComponent>();
        Debug.Log("���� �������� �������������");
    }

    public void Exit(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        entity.Del<FollowComponent>();
        Debug.Log("���� ���������� �������������");
    }

    public void Update(EcsEntity entity, GameObject enemyObject, EnemyStateMachine stateMachine)
    {
        Vector3 targetPosition = entity.Get<FollowComponent>().target.position;
        ref var attackComponent = ref entity.Get<EnemyAttackComponent>();

        if (Vector3.Distance(enemyObject.transform.position, targetPosition) < (attackComponent.range - 0.5f))
        {
            stateMachine.ChangeState<EnemyAttackState>(entity, enemyObject);
            Debug.Log("�������������->�����");
        }
    }
}
