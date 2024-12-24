using Leopotam.Ecs;
using UnityEngine;
public class HpRegenSystem : IEcsRunSystem
{
    private float _elapsedTimeToStartRegen = 0;
    private float _elapsedTimeToRegen = 0;
    private EcsFilter<DefenceComponent> _defenceFilter;
    private EcsFilter<DefenceComponent, TakeDamageEventComponent> _defenceTakeDamageFilter;

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
        foreach (var i in  _defenceTakeDamageFilter)
        {
            _elapsedTimeToStartRegen = 0;
            _canRegen = false;
            break;
        }

        foreach (var i in _defenceFilter)
        {
            ref var defenceComponent = ref _defenceFilter.Get1(i);
            ref var entity = ref _defenceFilter.GetEntity(i);

            if (defenceComponent.hp < defenceComponent.maxHP)
            {
                if ((_elapsedTimeToStartRegen += Time.deltaTime) >= defenceComponent.timeToStartHpRegenAfterTakeDamage)
                {
                    _canRegen = true;
                }
                if (_canRegen && ((_elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    defenceComponent.hp += defenceComponent.hpRegen;
                    entity.Get<PlayerHealthUpdateEventComponent>().newHealthPoints = defenceComponent.hp;

                    _elapsedTimeToRegen = 0;
                }
            }

            if (defenceComponent.hp == defenceComponent.maxHP)
            {
                _canRegen = false;
                _elapsedTimeToStartRegen = 0;
                _elapsedTimeToRegen = 0;
            }
        }
    }
}
