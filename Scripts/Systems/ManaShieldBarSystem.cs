using Leopotam.Ecs;
using System;

public class ManaShieldBarSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<DefenceComponent, ManaShieldUpdateEventComponent, PlayerHPManaShieldUpdateEventComponent> _playerManaShieldUpdateEventFilter;
    private EcsFilter<DefenceComponent, MaxManaShieldUpdateEventComponent, PlayerHPManaShieldUpdateEventComponent> _playerMaxManaShieldUpdateEventFilter;

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
            ref var defenceComponent = ref _playerManaShieldUpdateEventFilter.Get1(index);
            ref var PlayerManaShieldUpdateEventComponent = ref _playerManaShieldUpdateEventFilter.Get2(index);

            defenceComponent.manaShield = PlayerManaShieldUpdateEventComponent.newManaShield;
            _sceneData.manaShieldBar.Slider.value = defenceComponent.manaShield;
            _sceneData.manaShieldBar.Text.text = MathF.Round(defenceComponent.manaShield) + "/" + MathF.Round(defenceComponent.maxManaShield);
            entity.Del<ManaShieldUpdateEventComponent>();
        }
    }
    private void UpdateMaxManaShield()
    {
        foreach (var index in _playerMaxManaShieldUpdateEventFilter)
        {
            ref var entity = ref _playerMaxManaShieldUpdateEventFilter.GetEntity(index);
            ref var defenceComponent = ref _playerMaxManaShieldUpdateEventFilter.Get1(index);
            ref var playerMaxManaShieldUpdateEventComponent = ref _playerMaxManaShieldUpdateEventFilter.Get2(index);

            defenceComponent.maxManaShield = playerMaxManaShieldUpdateEventComponent.newMaxManaShield;
            _sceneData.manaShieldBar.Slider.maxValue = defenceComponent.maxManaShield;
            _sceneData.manaShieldBar.Text.text = MathF.Round(defenceComponent.manaShield) + "/" + MathF.Round(defenceComponent.maxManaShield);
            entity.Del<MaxManaShieldUpdateEventComponent>();
        }
    }
}
