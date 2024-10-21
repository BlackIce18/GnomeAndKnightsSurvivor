using Leopotam.Ecs;
using UnityEngine;

public struct EnemyComponent
{
    public ObjectPool<EnemyComponent> parentPool;
    public EcsEntity ecsEntity;
    public GameObject instance;
}
// Компонент для воина
public struct WarriorComponent : IEcsIgnoreInFilter { }

// Компонент для лучника
public struct ArcherComponent : IEcsIgnoreInFilter { }

// Компонент для мага
public struct MageComponent : IEcsIgnoreInFilter { }