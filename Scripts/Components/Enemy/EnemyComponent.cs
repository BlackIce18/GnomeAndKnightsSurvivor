using Leopotam.Ecs;

public struct EnemyComponent
{
    public ObjectPool<EnemyComponent> parentPool;
}
// Компонент для воина
public struct WarriorComponent : IEcsIgnoreInFilter { }

// Компонент для лучника
public struct ArcherComponent : IEcsIgnoreInFilter { }

// Компонент для мага
public struct MageComponent : IEcsIgnoreInFilter { }