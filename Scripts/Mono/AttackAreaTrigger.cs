using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

struct EnterAttackZoneEventComponent
{
    public int AttackerEntityId;
    public int TargetEntityId;
}

struct ExitAttackZoneEventComponent
{
    public int AttackerEntityId;
    public int TargetEntityId;
}

public class AttackAreaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject attackTargetPoint;
    private EcsWorld _world;
    [HideInInspector] public int AttackerEntityId;

    private void Start()
    {
        _world = new EcsWorld();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            var entity = _world.NewEntity();
            ref var eventComponent = ref entity.Get<EnterAttackZoneEventComponent>();
            eventComponent.AttackerEntityId = AttackerEntityId;
            eventComponent.TargetEntityId = other.GetInstanceID();
            Debug.Log("collider enter");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var entity = _world.NewEntity();
            ref var eventComponent = ref entity.Get<ExitAttackZoneEventComponent>();
            eventComponent.AttackerEntityId = AttackerEntityId;
            eventComponent.TargetEntityId = other.GetInstanceID();
            attackTargetPoint.transform.position = other.transform.position;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exit");
            attackTargetPoint.transform.position = Vector3.zero;
        }
    }
}
