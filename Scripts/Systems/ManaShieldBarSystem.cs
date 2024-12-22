using Leopotam.Ecs;

public class ManaShieldBarSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<PlayerManaShieldComponent> _playerManaShieldFilter;
    private EcsFilter<PlayerManaShieldComponent, PlayerManaShieldUpdateEventComponent> _playerManaShieldUpdateEventFilter;
    private EcsFilter<PlayerManaShieldComponent, PlayerMaxManaShieldUpdateEventComponent> _playerMaxManaShieldUpdateEventFilter;

    public void Init()
    {
        UpdateMaxManaShield();
        UpdateCurrentManaShield();
    }

    public void Run()
    {
        UpdateMaxManaShield();
        UpdateCurrentManaShield();
    }

    private void UpdateCurrentManaShield()
    {
        foreach (var index in _playerManaShieldUpdateEventFilter)
        {
            ref var entity = ref _playerManaShieldUpdateEventFilter.GetEntity(index);
            ref var playerManaShieldComponent = ref _playerManaShieldUpdateEventFilter.Get1(index);
            ref var PlayerManaShieldUpdateEventComponent = ref _playerManaShieldUpdateEventFilter.Get2(index);

            playerManaShieldComponent.currentManaShield = PlayerManaShieldUpdateEventComponent.newManaShield;
            _sceneData.manaShieldBar.Slider.value = playerManaShieldComponent.currentManaShield;
            _sceneData.manaShieldBar.Text.text = playerManaShieldComponent.currentManaShield + "/" + playerManaShieldComponent.maxManaShield;
            entity.Del<PlayerManaShieldUpdateEventComponent>();
        }
    }
    private void UpdateMaxManaShield()
    {
        foreach (var index in _playerMaxManaShieldUpdateEventFilter)
        {
            ref var entity = ref _playerMaxManaShieldUpdateEventFilter.GetEntity(index);
            ref var playerManaShieldComponent = ref _playerMaxManaShieldUpdateEventFilter.Get1(index);
            ref var playerMaxManaShieldUpdateEventComponent = ref _playerMaxManaShieldUpdateEventFilter.Get2(index);

            playerManaShieldComponent.maxManaShield = playerMaxManaShieldUpdateEventComponent.newMaxManaShield;
            _sceneData.manaShieldBar.Slider.maxValue = playerManaShieldComponent.maxManaShield;
            _sceneData.manaShieldBar.Text.text = playerManaShieldComponent.currentManaShield + "/" + playerManaShieldComponent.maxManaShield;
            entity.Del<PlayerMaxManaShieldUpdateEventComponent>();
        }
    }
}
