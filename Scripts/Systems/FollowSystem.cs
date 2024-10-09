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
            
            movableComponent.transform.Translate(-difference * movableComponent.speed * Time.deltaTime);
        }
    }
}
