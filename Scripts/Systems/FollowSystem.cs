using Leopotam.Ecs;
using UnityEngine;

public class FollowSystem : IEcsRunSystem
{
    private EcsFilter<FollowComponent, MovableComponent> _filter;

    public void Run()
    {
        foreach (var component in _filter)
        {
            ref var followComponent = ref _filter.Get1(component);
            ref var movableComponent = ref _filter.Get2(component);

            Vector3 difference = movableComponent.transform.position - followComponent.target.position;
            difference.y = 0;
            //Vector3 position = new Vector3(followComponent.target.transform.position.x, movableComponent.transform.position.y, followComponent.target.transform.position.z);
            movableComponent.transform.Translate(-difference * (movableComponent.speed / 10) * Time.deltaTime);
            /* 
             Vector3 startPosition = movableComponent.transform.position;
             Vector3 endPosition = followComponent.target.transform.position;
             float time = (movableComponent.speed / 10) * Time.deltaTime;
             movableComponent.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            */


            /*
             
            // при работе с сущностями нужно всегда сначала удостовериться, не уничтожены ли они
            if (!follow.target.IsAlive())
            {
                ref var entity = ref followingEnemies.GetEntity(i);
                animatorRef.animator.SetBool("Running", false);
                entity.Del<Follow>();
                continue;
            }
            
            ref var transformRef = ref follow.target.Get<TransformRef>();
            var targetPos = transformRef.transform.position;
            enemy.navMeshAgent.SetDestination(targetPos);
            var direction = (targetPos - enemy.transform.position).normalized;
            direction.y = 0f;
            enemy.transform.forward = direction;

            if ((enemy.transform.position - transformRef.transform.position).sqrMagnitude <
                enemy.meleeAttackDistance * enemy.meleeAttackDistance && Time.time >= follow.nextAttackTime)
            {
                follow.nextAttackTime = Time.time + enemy.meleeAttackInterval;
                animatorRef.animator.SetTrigger("Attack");
                ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                e.target = follow.target;
                e.value = enemy.damage;
            }
             */

        }
    }
}
