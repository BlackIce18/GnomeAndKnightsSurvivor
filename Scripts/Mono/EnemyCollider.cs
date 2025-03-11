using Leopotam.Ecs;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public EcsEntity entity;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GlobalEnvironment.playerAttackTag))
        {
            ref OnTriggerEnterComponent trigger = ref entity.Get<OnTriggerEnterComponent>();
            trigger.first = gameObject;
            trigger.other = other.gameObject;
        }
    }
}
