using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaShieldRegenSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private float _elapsedTimeToStartRegen = 0;
    private float _elapsedTimeToRegen = 0;
    private EcsFilter<DefenceComponent> _defenceFilter;
    private EcsFilter<DefenceComponent, TakeDamageEventComponent> _defenceTakeDamageFilter;

    private bool _canRegen = true;

    private bool _test = true;

    public void Run()
    {
        if (_test)
        {
            ref var _playerCharacteristicsFilterEntity = ref _defenceFilter.GetEntity(0);
            _playerCharacteristicsFilterEntity.Get<TakeDamageEventComponent>().damage = 150;
            _playerCharacteristicsFilterEntity.Get<TakeDamageEventComponent>().attackType = new MagicAttack(_sceneData.magicTypeAttackData);
            _playerCharacteristicsFilterEntity.Get<DefenceComponent>();
            _test = false;
        }
        foreach (var i in _defenceTakeDamageFilter)
        {
            _elapsedTimeToStartRegen = 0;
            _canRegen = false;
            break;
        }

        foreach (var i in _defenceFilter)
        {
            ref var defenceComponent = ref _defenceFilter.Get1(i);
            ref var entity = ref _defenceFilter.GetEntity(i);

            if (defenceComponent.manaShield < defenceComponent.maxManaShield)
            {
                if ((_elapsedTimeToStartRegen += Time.deltaTime) >= defenceComponent.timeToStartManaShieldRegenAfterTakeDamage)
                {
                    _canRegen = true;
                }
                if (_canRegen && ((_elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    defenceComponent.manaShield += defenceComponent.manaShieldRegen;
                    entity.Get<PlayerManaShieldUpdateEventComponent>().newManaShield = defenceComponent.manaShield;

                    _elapsedTimeToRegen = 0;
                }
            }

            if (defenceComponent.manaShield == defenceComponent.maxManaShield)
            {
                _canRegen = false;
                _elapsedTimeToStartRegen = 0;
                _elapsedTimeToRegen = 0;
            }
        }
    }
}
