using Leopotam.Ecs;


public class PlayerTakeDamageSystem : IEcsRunSystem
{
    private EcsFilter<PlayerHealthComponent, TakeDamageEventComponent> _playerHealthFilter;

    public void Run()
    {
        foreach(var index in _playerHealthFilter)
        {
            ref var playerHealth = ref _playerHealthFilter.Get1(index);
            ref var damage = ref _playerHealthFilter.Get2(index);
            ref var playerHealthEntity = ref _playerHealthFilter.GetEntity(index);

            ref var playerHealthUpdateEvent = ref playerHealthEntity.Get<PlayerHealthUpdateEventComponent>();
            playerHealthUpdateEvent.newHealthPoints = playerHealth.currentHealthPoints - damage.damage;

            playerHealthEntity.Del<TakeDamageEventComponent>();
        }
    }
}
