using Leopotam.Ecs;
using UnityEngine;

public interface IEnemyBehavior
{
    void AddBehavior(EcsEntity entity, GameObject gameObject, EnemyData data);
}