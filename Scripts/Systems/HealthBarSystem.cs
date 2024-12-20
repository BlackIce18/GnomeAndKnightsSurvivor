using Leopotam.Ecs;

public class HealthBarSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<PlayerHealthComponent> _playerHealthFilter;
    private EcsFilter<PlayerHealthComponent, PlayerHealthUpdateEventComponent> _playerHealthUpdateEventFilter;
    private EcsFilter<PlayerHealthComponent, PlayerMaxHealthUpdateEventComponent> _playerMaxHealthUpdateEventFilter;

    public void Init()
    {
        UpdateMaxHealth();
        UpdateCurrentHealth();
    }

    public void Run()
    {
        UpdateMaxHealth();
        UpdateCurrentHealth();
    }

    private void UpdateCurrentHealth()
    {
        foreach (var index in _playerHealthUpdateEventFilter)
        {
            ref var entity = ref _playerHealthUpdateEventFilter.GetEntity(index);
            ref var playerHealthComponent = ref _playerHealthUpdateEventFilter.Get1(index);
            ref var PlayerHealthUpdateEventComponent = ref _playerHealthUpdateEventFilter.Get2(index);

            playerHealthComponent.currentHealth = PlayerHealthUpdateEventComponent.newHealth;
            _sceneData.healthBar.Slider.value = playerHealthComponent.currentHealth;
            _sceneData.healthBar.Text.text = playerHealthComponent.currentHealth + "/" + playerHealthComponent.maxHealth;
            entity.Del<PlayerHealthUpdateEventComponent>();
        }
    }
    private void UpdateMaxHealth()
    {
        foreach (var index in _playerMaxHealthUpdateEventFilter)
        {
            ref var entity = ref _playerMaxHealthUpdateEventFilter.GetEntity(index);
            ref var playerHealthComponent = ref _playerMaxHealthUpdateEventFilter.Get1(index);
            ref var playerMaxHealthUpdateEventComponent = ref _playerMaxHealthUpdateEventFilter.Get2(index);

            playerHealthComponent.maxHealth = playerMaxHealthUpdateEventComponent.newMaxHealth;
            _sceneData.healthBar.Slider.maxValue = playerHealthComponent.maxHealth;
            _sceneData.healthBar.Text.text = playerHealthComponent.currentHealth + "/" + playerHealthComponent.maxHealth;
            entity.Del<PlayerMaxHealthUpdateEventComponent>();
        }
    }
}
