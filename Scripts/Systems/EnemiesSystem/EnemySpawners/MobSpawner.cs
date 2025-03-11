using Leopotam.Ecs;
using UnityEngine;

public abstract class MobSpawner
{
    protected IEnemyBehavior behavior;
    protected EcsWorld world;
    protected ObjectPool<EnemyComponent> pool;
    protected SceneData sceneData;
    protected EcsEntity enemyEntity;
    protected GameObject enemyObject;
    protected EnemyData enemyData;

    public MobSpawner(IEnemyBehavior behavior)
    {
        this.behavior = behavior;
    }

    public MobSpawner(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool)
    {
        this.world = world;
        this.sceneData = sceneData;
        this.pool = pool;
    }

    public MobSpawner(EcsWorld world, SceneData sceneData, ObjectPool<EnemyComponent> pool, IEnemyBehavior behavior)
    {
        this.world = world;
        this.sceneData = sceneData;
        this.pool = pool;
        this.behavior = behavior;
    }

    public virtual void Create(EnemyData enemyData, Vector3 position) { }

    protected void AddBehavior(EcsEntity entity, GameObject gameObject, EnemyData data)
    {
        behavior.AddBehavior(entity, gameObject, data);
    }

    protected void AddMoveFunction()
    {
        ref MovableComponent _movableComponent = ref enemyEntity.Get<MovableComponent>();
        _movableComponent.transform = enemyObject.transform;
        _movableComponent.speed = enemyData.speed;
    }

    protected void AddFollowFunction()
    {
        if (sceneData.player == null)
        {
            Debug.LogError("Ошибка: sceneData.player == null при инициализации FollowComponent!");
        }

        ref FollowComponent _followComponent = ref enemyEntity.Get<FollowComponent>();
        _followComponent.target = sceneData.player;
    }

    protected void InitNew(EnemyData eData, Vector3 position)
    {
        CreateEntity(eData);
        InitGameObject(position);
        InitDefenceComponent();
        EnemyComponentInit();
    }

    private void CreateEntity(EnemyData eData)
    {
        enemyEntity = world.NewEntity();
        enemyData = eData;
    }

    private void InitGameObject(Vector3 position)
    {
        enemyObject = GameObject.Instantiate(enemyData.prefab, position, enemyData.prefab.transform.rotation, sceneData.enemyParentObject);
    }

    private void InitDefenceComponent()
    {
        ref DefenceComponent defenceComponent = ref enemyEntity.Get<DefenceComponent>();
        defenceComponent.hp = enemyData.defenceComponent.hp;
        defenceComponent.maxHP = enemyData.defenceComponent.maxHP;
        defenceComponent.manaShield = enemyData.defenceComponent.manaShield;
        defenceComponent.maxManaShield = enemyData.defenceComponent.maxManaShield;
        defenceComponent.armor = enemyData.defenceComponent.armor;
        defenceComponent.hpRegen = enemyData.defenceComponent.hpRegen;
        defenceComponent.manaShieldRegen = enemyData.defenceComponent.manaShieldRegen;
        defenceComponent.timeToStartHpRegenAfterTakeDamage = enemyData.defenceComponent.timeToStartHpRegenAfterTakeDamage;
        defenceComponent.timeToStartManaShieldRegenAfterTakeDamage = enemyData.defenceComponent.timeToStartManaShieldRegenAfterTakeDamage;
        defenceComponent.armorType = enemyData.defenceComponent.armorType;
    }

    private void EnemyComponentInit()
    {
        ref EnemyAttackComponent attackComponent = ref enemyEntity.Get<EnemyAttackComponent>();
        attackComponent.attackRange = enemyData.attackComponent.attackRange;
        attackComponent.damage = enemyData.attackComponent.damage;
        attackComponent.range = enemyData.attackComponent.range;
        attackComponent.viewRange = enemyData.attackComponent.viewRange;
        attackComponent.attackRate = enemyData.attackComponent.attackRate;
        attackComponent.critChance = enemyData.attackComponent.critChance;
        attackComponent.critDamageMultiplier = enemyData.attackComponent.critDamageMultiplier;

        ref EnemyComponent enemyComponent = ref enemyEntity.Get<EnemyComponent>();
        enemyComponent.parentPool = pool;
        enemyComponent.ecsEntity = enemyEntity;
        enemyComponent.instance = enemyObject;
        enemyComponent.enemyData = enemyData;

        ref EnemyStateComponent enemyState = ref enemyEntity.Get<EnemyStateComponent>();

        EnemyCollider enemyCollider = enemyObject.GetComponent<EnemyCollider>();
        enemyCollider.entity = enemyEntity;

        AttackAreaTrigger attackAreaTrigger = enemyObject.GetComponent<AttackAreaTrigger>();
        if (attackAreaTrigger != null)
        {
            attackAreaTrigger.AttackerEntityId = enemyEntity.GetHashCode();
        }
    }

    protected bool InitFromPool(EnemyData enemyData, Vector3 position)
    {
        EnemyComponent enemyComponentFromPool = pool.GetFromPool();
        if (enemyComponentFromPool.instance != null)
        {
            enemyComponentFromPool.instance.transform.position = position;
            enemyComponentFromPool.instance.SetActive(true);

            ref DefenceComponent defenceComponent = ref enemyComponentFromPool.ecsEntity.Get<DefenceComponent>();
            defenceComponent.hp = enemyData.defenceComponent.hp;

            enemyEntity = enemyComponentFromPool.ecsEntity;
            enemyObject = enemyComponentFromPool.instance;

            return true;
        }

        return false;
    }
}