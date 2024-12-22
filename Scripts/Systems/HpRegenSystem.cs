using Leopotam.Ecs;
using UnityEngine;

public struct BasicPlayerCharacteristicsComponent
{
    public int HP;
    public int maxHP;
    public int ManaShield;
    public int maxManaShield;
    public float playerSpeed;
    public int Armor;
    public int HpRegen;
    public int ManaShieldRegen;
    public float timeToStartHpRegenAfterTakeDamage;
    public float timeToStartManaShieldRegenAfterTakeDamage;
}

public struct CurrentPlayerCharacteristicsComponent
{
    public int HP;
    public int maxHP;
    public int ManaShield;
    public int maxManaShield;
    public float playerSpeed;
    public int Armor;
    public int HpRegen;
    public int ManaShieldRegen;
    public float timeToStartHpRegenAfterTakeDamage;
    public float timeToStartManaShieldRegenAfterTakeDamage;
}


public class HpRegenSystem : IEcsRunSystem
{
    private float _elapsedTimeToStartRegen = 0;
    private float _elapsedTimeToRegen = 0;
    private EcsFilter<PlayerHealthComponent, CurrentPlayerCharacteristicsComponent> _playerCharacteristicsFilter;
    private EcsFilter<PlayerHealthComponent, TakeDamageEventComponent> _playerHealthFilter;

    private bool _canRegen = true;

    public void Run()
    {
        foreach(var i in  _playerHealthFilter)
        {
            _elapsedTimeToStartRegen = 0;
            _canRegen = false;
            break;
        }

        foreach (var i in _playerCharacteristicsFilter)
        {
            ref var playerHp = ref _playerCharacteristicsFilter.Get1(i);
            ref var playerCharacteristics = ref _playerCharacteristicsFilter.Get2(i);

            if (playerHp.currentHealth < playerHp.maxHealth)
            {
                if ((_elapsedTimeToStartRegen += Time.deltaTime) >= playerCharacteristics.timeToStartHpRegenAfterTakeDamage)
                {
                    _canRegen = true;
                }
                if (_canRegen && ((_elapsedTimeToRegen += Time.deltaTime) >= 1))
                {
                    playerHp.currentHealth += playerCharacteristics.HpRegen;

                    _elapsedTimeToRegen = 0;
                }
            }

            if (playerHp.currentHealth == playerHp.maxHealth)
            {
                _canRegen = false;
                _elapsedTimeToStartRegen = 0;
                _elapsedTimeToRegen = 0;
            }
        }
    }
}
