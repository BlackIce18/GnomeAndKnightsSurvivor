using Leopotam.Ecs;
using UnityEngine;
public class HpRegenSystem : IEcsRunSystem
{
    private float _elapsedTimeToStartRegen = 0;
    private float _elapsedTimeToRegen = 0;
    private EcsFilter<PlayerHealthComponent, CurrentPlayerCharacteristicsComponent> _playerCharacteristicsFilter;
    private EcsFilter<PlayerHealthComponent, TakeDamageEventComponent> _playerHealthFilter;

    private bool _canRegen = true;

    //private bool _test = true;

    public void Run()
    {
        /*if (_test)
        {
            ref var _playerCharacteristicsFilterEntity = ref _playerCharacteristicsFilter.GetEntity(0);
            _playerCharacteristicsFilterEntity.Get<TakeDamageEventComponent>().damage = 150;
            _playerCharacteristicsFilterEntity.Get<PlayerHealthComponent>();
            _test = false;
        }*/
        foreach (var i in  _playerHealthFilter)
        {
            _elapsedTimeToStartRegen = 0;
            _canRegen = false;
            break;
        }

        foreach (var i in _playerCharacteristicsFilter)
        {
            ref var playerHp = ref _playerCharacteristicsFilter.Get1(i);
            ref var playerCharacteristics = ref _playerCharacteristicsFilter.Get2(i);
            ref var entity = ref _playerCharacteristicsFilter.GetEntity(i);

            if (playerHp.currentHealthPoints < playerHp.maxHealthPoints)
            {
                if ((_elapsedTimeToStartRegen += Time.deltaTime) >= playerCharacteristics.timeToStartHpRegenAfterTakeDamage)
                {
                    _canRegen = true;
                }
                if (_canRegen && ((_elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    playerHp.currentHealthPoints += playerCharacteristics.healthPointRegen;
                    entity.Get<PlayerHealthUpdateEventComponent>().newHealthPoints = playerHp.currentHealthPoints;

                    _elapsedTimeToRegen = 0;
                }
            }

            if (playerHp.currentHealthPoints == playerHp.maxHealthPoints)
            {
                _canRegen = false;
                _elapsedTimeToStartRegen = 0;
                _elapsedTimeToRegen = 0;
            }
        }
    }
}
