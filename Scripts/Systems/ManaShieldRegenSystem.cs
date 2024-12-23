using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaShieldRegenSystem : IEcsRunSystem
{
    private float _elapsedTimeToStartRegen = 0;
    private float _elapsedTimeToRegen = 0;
    private EcsFilter<PlayerManaShieldComponent, CurrentPlayerCharacteristicsComponent> _playerCharacteristicsFilter;
    private EcsFilter<PlayerManaShieldComponent, TakeDamageEventComponent> _playerManaShieldFilter;

    private bool _canRegen = true;

    private bool _test = true;

    public void Run()
    {
        if (_test)
        {
            ref var _playerCharacteristicsFilterEntity = ref _playerCharacteristicsFilter.GetEntity(0);
            _playerCharacteristicsFilterEntity.Get<TakeDamageEventComponent>().damage = 150;
            _playerCharacteristicsFilterEntity.Get<PlayerManaShieldComponent>();
            _test = false;
        }
        foreach (var i in _playerManaShieldFilter)
        {
            _elapsedTimeToStartRegen = 0;
            _canRegen = false;
            break;
        }

        foreach (var i in _playerCharacteristicsFilter)
        {
            ref var playerManaSield = ref _playerCharacteristicsFilter.Get1(i);
            ref var playerCharacteristics = ref _playerCharacteristicsFilter.Get2(i);
            ref var entity = ref _playerCharacteristicsFilter.GetEntity(i);

            if (playerManaSield.currentManaShield < playerManaSield.maxManaShield)
            {
                if ((_elapsedTimeToStartRegen += Time.deltaTime) >= playerCharacteristics.timeToStartManaShieldRegenAfterTakeDamage)
                {
                    _canRegen = true;
                }
                if (_canRegen && ((_elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    playerManaSield.currentManaShield += playerCharacteristics.healthPointRegen;
                    entity.Get<PlayerManaShieldUpdateEventComponent>().newManaShield = playerManaSield.currentManaShield;

                    _elapsedTimeToRegen = 0;
                }
            }

            if (playerManaSield.currentManaShield == playerManaSield.maxManaShield)
            {
                _canRegen = false;
                _elapsedTimeToStartRegen = 0;
                _elapsedTimeToRegen = 0;
            }
        }
    }
}
