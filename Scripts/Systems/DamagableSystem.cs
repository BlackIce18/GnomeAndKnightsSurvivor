using Leopotam.Ecs;
using UnityEngine;

public class DamagableSystem : IEcsRunSystem
{
    private EcsFilter<DefenceComponent, OnTriggerEnterComponent> _filter = null;
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var defenceComponent = ref _filter.Get1(i);
            ref var OnTriggerEnterComponent = ref _filter.Get2(i);
            if ((OnTriggerEnterComponent.first != null) && 
                (OnTriggerEnterComponent.other != null))
            { 
                if(OnTriggerEnterComponent.first.CompareTag("Enemy") &&
                   OnTriggerEnterComponent.other.CompareTag("Bullet")) 
                {
                    Debug.Log(defenceComponent.hp);
                    Debug.Log("True");
                }
            }

            _filter.GetEntity(i).Del<OnTriggerEnterComponent>();
        }
    }
}
