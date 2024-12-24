using Leopotam.Ecs;
using System;

public class PlayerDamageableSystem : IEcsRunSystem
{
    private EcsFilter<DefenceComponent, TakeDamageEventComponent> _playerHealthFilter;

    public void Run()
    {
        foreach (var index in _playerHealthFilter)
        {
            ref DefenceComponent defenceComponent = ref _playerHealthFilter.Get1(index);
            ref TakeDamageEventComponent damage = ref _playerHealthFilter.Get2(index);
            ref EcsEntity entity = ref _playerHealthFilter.GetEntity(index);
            var resultDamage = defenceComponent.hp - damage.damage;

            if (defenceComponent.manaShield > 0)
            {
                if (damage.attackType is MagicAttack) 
                {
                    ref var playerManaShieldUpdateEvent = ref entity.Get<PlayerManaShieldUpdateEventComponent>();
                    var difference = defenceComponent.manaShield - damage.damage;

                    if (difference > 0)
                    {
                        playerManaShieldUpdateEvent.newManaShield = difference;
                    }
                    else if (difference < 0)
                    {
                        playerManaShieldUpdateEvent.newManaShield = 0;
                        difference = Math.Abs(difference);
                        resultDamage = difference;
                    }
                }
            }
            ref var playerHealthUpdateEvent = ref entity.Get<PlayerHealthUpdateEventComponent>();
            playerHealthUpdateEvent.newHealthPoints = resultDamage;

            entity.Del<TakeDamageEventComponent>();
        }
    }
}
