using Leopotam.Ecs;

public struct EnemyComponent
{
    public ObjectPool<EnemyComponent> parentPool;
}
// ��������� ��� �����
public struct WarriorComponent : IEcsIgnoreInFilter { }

// ��������� ��� �������
public struct ArcherComponent : IEcsIgnoreInFilter { }

// ��������� ��� ����
public struct MageComponent : IEcsIgnoreInFilter { }