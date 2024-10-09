using Leopotam.Ecs;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public EcsWorld world;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            var entity = world.NewEntity();
            ref var triggerComponent = ref entity.Get<OnTriggerEnterComponent>();
            ref var defenceComponent = ref entity.Get<DefenceComponent>();
            triggerComponent.first = gameObject;
            triggerComponent.other = other.gameObject;

            Debug.Log(gameObject.transform.tag);
            Debug.Log(other.gameObject.transform.tag);
        }
    }
}
