using Leopotam.Ecs;
using UnityEngine;

public struct EnemyComponent
{
    public ObjectPool<EnemyComponent> parentPool;
    public EcsEntity ecsEntity;
    public GameObject instance;
}
// ��������� ��� �����
public struct WarriorComponent : IEcsIgnoreInFilter { }

// ��������� ��� �������
public struct ArcherComponent : IEcsIgnoreInFilter { }

// ��������� ��� ����
public struct MageComponent : IEcsIgnoreInFilter { }