using Leopotam.Ecs;
using System;

public class PlayerDamageableSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private bool _test = true;
    private EcsFilter<DefenceComponent, UserComponent> _filter;
    private EcsFilter<DefenceComponent, UserComponent, TakeDamageEventComponent> _defenceFilter;

    public void Run()
    {
        if (_test)
        {
            ref var _playerCharacteristicsFilterEntity = ref _filter.GetEntity(0);
            ref var takeDamageEvent = ref _playerCharacteristicsFilterEntity.Get<TakeDamageEventComponent>();
            takeDamageEvent.damage = 15;
            takeDamageEvent.attackType = new MagicAttack(_sceneData.magicTypeAttackData);
            _test = false;
        }
        foreach (var index in _defenceFilter)
        {
            ref DefenceComponent defenceComponent = ref _defenceFilter.Get1(index);
            ref TakeDamageEventComponent damage = ref _defenceFilter.Get3(index);
            ref EcsEntity entity = ref _defenceFilter.GetEntity(index);
            var resultHp = defenceComponent.hp;

            if (defenceComponent.manaShield > 0)
            {
                if (damage.attackType is MagicAttack) 
                {
                    ref var playerManaShieldUpdateEvent = ref entity.Get<ManaShieldUpdateEventComponent>();
                    var difference = defenceComponent.manaShield - damage.damage;

                    if (difference > 0)
                    {
                        playerManaShieldUpdateEvent.newManaShield = defenceComponent.manaShield = difference;
                    }
                    else if (difference < 0)
                    {
                        playerManaShieldUpdateEvent.newManaShield = defenceComponent.manaShield = 0;
                        difference = Math.Abs(difference);
                        resultHp = defenceComponent.hp -= difference;
                    }
                }
            }
            ref var playerHealthUpdateEvent = ref entity.Get<HpUpdateEventComponent>();
            playerHealthUpdateEvent.newHealthPoints = resultHp;

            entity.Del<TakeDamageEventComponent>();
        }
    }
}
