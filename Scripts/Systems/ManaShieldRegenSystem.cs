using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaShieldRegenSystem : IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<DefenceComponent, ManaShieldRegenComponent> _defenceFilter;
    private EcsFilter<DefenceComponent, ManaShieldRegenComponent, TakeDamageEventComponent> _defenceTakeDamageFilter;



    public void Run()
    {


        foreach (var i in _defenceTakeDamageFilter)
        {
            ref var manaShieldRegenComponent = ref _defenceTakeDamageFilter.Get2(i);
            manaShieldRegenComponent.elapsedTimeToStartRegen = 0;
            manaShieldRegenComponent.canRegen = false;
            //break;
        }

        foreach (var i in _defenceFilter)
        {
            ref var defenceComponent = ref _defenceFilter.Get1(i);
            ref var entity = ref _defenceFilter.GetEntity(i);
            ref var ManaShieldRegenComponent = ref _defenceFilter.Get2(i);


            if (defenceComponent.manaShield < defenceComponent.maxManaShield)
            {
                if ((ManaShieldRegenComponent.elapsedTimeToStartRegen += Time.deltaTime) >= defenceComponent.timeToStartManaShieldRegenAfterTakeDamage)
                {
                    ManaShieldRegenComponent.canRegen = true;
                }
                if (ManaShieldRegenComponent.canRegen && ((ManaShieldRegenComponent.elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    defenceComponent.manaShield = ((defenceComponent.manaShield + defenceComponent.manaShieldRegen) >= defenceComponent.maxManaShield) ? defenceComponent.maxManaShield : defenceComponent.manaShield += defenceComponent.manaShieldRegen;
                    entity.Get<ManaShieldUpdateEventComponent>().newManaShield = defenceComponent.manaShield;

                    ManaShieldRegenComponent.elapsedTimeToRegen = 0;
                }
            }

            if (defenceComponent.manaShield == defenceComponent.maxManaShield)
            {
                ManaShieldRegenComponent.canRegen = false;
                ManaShieldRegenComponent.elapsedTimeToStartRegen = 0;
                ManaShieldRegenComponent.elapsedTimeToRegen = 0;
            }
        }
    }
}
