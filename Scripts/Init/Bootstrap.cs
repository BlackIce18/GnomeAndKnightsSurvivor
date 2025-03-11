using Leopotam.Ecs;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _enemySystems;
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private ActiveGuns _activeGuns;
    // Start is called before the first frame update
    void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems.Add(new InitSystem());
        _systems.Add(new GunsSpawnSystem());
        _systems.Add(new TimerSystem());
        _systems.Add(new ShopSystem());
        _systems.Add(new ResetShopSystem());
        _systems.Add(new MouseInputSystem());
        _systems.Add(new KeyboardInputSystem());
        _systems.Add(new PlayerMoveSystem());
        _systems.Add(new MoneyIncomeSystem());
        _systems.Add(new KillBountySystem());
        _systems.Add(new HpRegenSystem());
        _systems.Add(new ManaShieldRegenSystem());
        _systems.Add(new SingleBulletWeaponSystem());
        _systems.Add(new BulletMoverSystem());
        _systems.Add(new PlayerDamageableSystem());
        _systems.Add(new LifeTimeBulletsSystem());
        _systems.Add(new BorderColorsSystem());
        _systems.Add(new DamageTextSystem());
        _systems.Add(new XpSystem());
        _systems.Add(new LvlSystem());
        _systems.Add(new HpBarSystem());
        _systems.Add(new ManaShieldBarSystem());
        _systems.Add(new WalletUpdateUISystem());
        _systems.Inject(_sceneData);
        _systems.Inject(_activeGuns);
        _systems.Init();


        _enemySystems = new EcsSystems(_world);
        _enemySystems.Add(new EnemyInitSystem());
        _enemySystems.Add(new FollowSystem());
        _enemySystems.Add(new EnemyDamageableSystem());
        _enemySystems.Add(new EnemySpawnerSystem());
        _enemySystems.Add(new EnemyStateSystem());
        _enemySystems.Add(new RemoveEnemySystem());
        _enemySystems.Inject(_sceneData);
        _enemySystems.Init();
    }

    private void Update()
    {
        _systems?.Run();
        _enemySystems?.Run();
    }
    private void OnDestroy()
    {
        _systems.Destroy();
        _enemySystems.Destroy();
        _world.Destroy();
    }
}
