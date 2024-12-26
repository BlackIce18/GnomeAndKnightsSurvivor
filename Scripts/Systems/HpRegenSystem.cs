using Leopotam.Ecs;
using UnityEngine;
public class HpRegenSystem : IEcsRunSystem
{
    private EcsFilter<DefenceComponent, HPRegenComponent> _defenceFilter;
    private EcsFilter<DefenceComponent, HPRegenComponent, TakeDamageEventComponent> _defenceTakeDamageFilter;

    public void Run()
    {
        foreach (var i in  _defenceTakeDamageFilter)
        {
            ref var hpRegenComponent = ref _defenceTakeDamageFilter.Get2(i);
            hpRegenComponent.elapsedTimeToStartRegen = 0;
            hpRegenComponent.canRegen = false;
            break;
        }

        foreach (var i in _defenceFilter)
        {
            ref var hpRegenComponent = ref _defenceTakeDamageFilter.Get2(i);
            ref var defenceComponent = ref _defenceFilter.Get1(i);
            ref var entity = ref _defenceFilter.GetEntity(i);

            if (defenceComponent.hp < defenceComponent.maxHP)
            {
                if ((hpRegenComponent.elapsedTimeToStartRegen += Time.deltaTime) >= defenceComponent.timeToStartHpRegenAfterTakeDamage)
                {
                    hpRegenComponent.canRegen = true;
                }
                if (hpRegenComponent.canRegen && ((hpRegenComponent.elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    defenceComponent.hp = ((defenceComponent.hp + defenceComponent.hpRegen) >= defenceComponent.maxHP) ?  defenceComponent.maxHP : defenceComponent.hp += defenceComponent.hpRegen;
                    entity.Get<HpUpdateEventComponent>().newHealthPoints = defenceComponent.hp;

                    hpRegenComponent.elapsedTimeToRegen = 0;
                }
            }

            if (defenceComponent.hp == defenceComponent.maxHP)
            {
                hpRegenComponent.canRegen = false;
                hpRegenComponent.elapsedTimeToStartRegen = 0;
                hpRegenComponent.elapsedTimeToRegen = 0;
            }
        }
    }
}
