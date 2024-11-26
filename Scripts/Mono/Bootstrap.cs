using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private EnemiesData _enemyData;
    [SerializeField] private ActiveGuns _activeGuns;
    // Start is called before the first frame update
    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.Add(new InitSystem());
        _systems.Add(new GunsSpawnSystem());
        _systems.Add(new MouseInputSystem());
        _systems.Add(new KeyboardInputSystem());
        _systems.Add(new PlayerMoveSystem());
        _systems.Add(new PlayerMoneyIncomeSystem());
        _systems.Add(new TimerSystem());
        _systems.Add(new FireballAttackSystem());
        _systems.Add(new BulletMoverSystem());
        _systems.Add(new FollowSystem());
        _systems.Add(new DamageTextSystem());
        _systems.Add(new EnemyDamageableSystem());
        _systems.Add(new LifeTimeBulletsSystem());
        _systems.Add(new EnemySpawnerSystem());
        _systems.Add(new RemoveEnemySystem());
        _systems.Add(new ShopSystem());
        _systems.Add(new ResetShopSystem());
        _systems.Add(new WalletUpdateUISystem());
        _systems.Add(new GunSpawnerSystem());
        _systems.Inject(_sceneData);
        _systems.Inject(_enemyData);
        _systems.Inject(_activeGuns);
        _systems.Init();
    }

    private void Update()
    {
        _systems?.Run();
    }
    private void OnDestroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }
}
