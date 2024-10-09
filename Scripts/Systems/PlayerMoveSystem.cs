using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private EcsFilter<MovableComponent, UserInputComponent> _filter;

    public void Run()
    {
        foreach (var component in _filter)
        {
            ref MovableComponent moveComponent = ref _filter.Get1(component);
            ref UserInputComponent userInputComponent = ref _filter.Get2(component);
            Transform transform = moveComponent.transform;

            var x = userInputComponent.moveX;
            var z = userInputComponent.moveY;

            Vector3 movement = Vector3.ClampMagnitude(new Vector3(x, 0, z), 1);

            transform.Translate(movement * moveComponent.speed * Time.deltaTime);
        }
    }
}
