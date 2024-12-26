using Leopotam.Ecs;
using System;

public class HpBarSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<DefenceComponent, HpUpdateEventComponent, PlayerHPManaShieldUpdateEventComponent> _playerHealthUpdateEventFilter;
    private EcsFilter<DefenceComponent, MaxHpUpdateEventComponent, PlayerHPManaShieldUpdateEventComponent> _playerMaxHealthUpdateEventFilter;

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

            playerHealthComponent.hp = PlayerHealthUpdateEventComponent.newHealthPoints;
            _sceneData.healthBar.Slider.value = playerHealthComponent.hp;
            _sceneData.healthBar.Text.text = MathF.Round(playerHealthComponent.hp) + "/" + MathF.Round(playerHealthComponent.maxHP);
            entity.Del<HpUpdateEventComponent>();
        }
    }
    private void UpdateMaxHealth()
    {
        foreach (var index in _playerMaxHealthUpdateEventFilter)
        {
            ref var entity = ref _playerMaxHealthUpdateEventFilter.GetEntity(index);
            ref var playerHealthComponent = ref _playerMaxHealthUpdateEventFilter.Get1(index);
            ref var playerMaxHealthUpdateEventComponent = ref _playerMaxHealthUpdateEventFilter.Get2(index);

            playerHealthComponent.maxHP = playerMaxHealthUpdateEventComponent.newMaxHealthPoints;
            _sceneData.healthBar.Slider.maxValue = playerHealthComponent.maxHP;
            _sceneData.healthBar.Text.text = MathF.Round(playerHealthComponent.hp) + "/" + MathF.Round(playerHealthComponent.maxHP);
            entity.Del<MaxHpUpdateEventComponent>();
        }
    }
}
