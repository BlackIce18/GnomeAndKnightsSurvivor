using Leopotam.Ecs;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyCollider : MonoBehaviour
{
    public EcsEntity entity;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ref OnTriggerEnterComponent trigger = ref entity.Get<OnTriggerEnterComponent>();
            trigger.first = gameObject;
            trigger.other = other.gameObject;
        }
    }
}
