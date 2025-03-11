using Leopotam.Ecs;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class EnemySpawnerFactory
{
    private static EcsWorld _world;
    private static SceneData _sceneData;
    private static ObjectPool<EnemyComponent> _pool;

    private static Dictionary<string, Func<EcsWorld, SceneData, ObjectPool<EnemyComponent>, IEnemySpawner>> spawners =
        new Dictionary<string, Func<EcsWorld, SceneData, ObjectPool<EnemyComponent>, IEnemySpawner>>();

    public static void Initialize(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool)
    {
        _world = world;
        _sceneData = sceneData;
        _pool = pool;

        RegisterSpawner("Knight", (world, sceneData, pool) => new KnightSpawnerSystem(world, sceneData, pool));
    }

    public static void RegisterSpawner(string type, Func<EcsWorld, SceneData, ObjectPool<EnemyComponent>, IEnemySpawner> factory)
    {
        spawners[type] = factory;
    }
    public static void SpawnEnemy(EnemyData enemyData, Vector3 position)
    {
        if (!spawners.TryGetValue(enemyData.name, out var spawnerFactory))
        {
            Debug.LogError($"Спавнер для типа {enemyData.name} не найден!");
            return;
        }

        IEnemySpawner spawner = spawnerFactory(_world, _sceneData, _pool);
        spawner.Create(enemyData, position);
    }
    public static void SpawnEnemy(string enemyType, UnityEngine.Vector3 position)
    {
        if (!spawners.TryGetValue(enemyType, out var spawnerFactory))
        {
            Debug.LogError($"Спавнер для типа {enemyType} не найден!");
            return;
        }

        EnemyData enemyData = GetEnemyData(enemyType);
        if (enemyData == null) return;

        IEnemySpawner spawner = spawnerFactory(_world, _sceneData, _pool);
        spawner.Create(enemyData, position);
    }

    private static EnemyData GetEnemyData(string enemyType)
    {
        EnemyData data = Resources.Load<EnemyData>($"Enemies/{enemyType}");
        if (data == null)
        {
            Debug.LogError($"EnemyData для {enemyType} не найдено в Resources/Enemies/");
        }
        return data;
    }
}

public class KnightSpawnerSystem : MobSpawner, IEnemySpawner
{
    public KnightSpawnerSystem(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool) : base(world, sceneData, pool) { }

    public override void Create(EnemyData enemyData, Vector3 position)
    {
        if (!InitFromPool(enemyData, position))
        {
            InitNew(enemyData, position);
        }

        AddMoveFunction();

        if(enemyData.canFollow)
        {
            AddFollowFunction();
        }
    }
}
public class KnightBehavior : IEnemyBehavior
{
    private SceneData sceneData;

    public KnightBehavior(SceneData sceneData)
    {
        this.sceneData = sceneData;
    }

    public void AddBehavior(EcsEntity entity, GameObject gameObject, EnemyData data)
    {
        ref MovableComponent movableComponent = ref entity.Get<MovableComponent>();
        movableComponent.transform = gameObject.transform;
        movableComponent.speed = data.speed;

        ref FollowComponent followComponent = ref entity.Get<FollowComponent>();
        followComponent.target = sceneData.player;
    }
}

public class ArcherSpawnerSystem : MobSpawner, IEnemySpawner
{
    public ArcherSpawnerSystem(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool) : base(world, sceneData, pool) { }


}

public class ArcherBehavior : IEnemyBehavior
{
    public void AddBehavior(EcsEntity entity, GameObject gameObject, EnemyData data)
    {
        ref RangedAttackComponent rangedAttackComponent = ref entity.Get<RangedAttackComponent>();
        rangedAttackComponent.range = data.attackComponent.range;
        rangedAttackComponent.projectilePrefab = data.prefab;
    }
}