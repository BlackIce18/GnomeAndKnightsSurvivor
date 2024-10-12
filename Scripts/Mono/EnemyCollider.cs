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
            entity.Get<OnTriggerEnterComponent>().first = gameObject;
            entity.Get<OnTriggerEnterComponent>().other = other.gameObject;
        }
    }
}
