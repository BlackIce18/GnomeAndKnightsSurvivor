using Leopotam.Ecs;

public struct HPRegenComponent
{
    public bool canRegen;
    public float elapsedTimeToStartRegen;
    public float elapsedTimeToRegen;
}
public struct ManaShieldRegenComponent
{
    public bool canRegen;
    public float elapsedTimeToStartRegen;
    public float elapsedTimeToRegen;
}
public struct PlayerHPManaShieldUpdateEventComponent : IEcsIgnoreInFilter { }
public struct HpUpdateEventComponent
{
    public float newHealthPoints;
}
public struct MaxHpUpdateEventComponent
{
    public float newMaxHealthPoints;
}

public struct ManaShieldUpdateEventComponent
{
    public float newManaShield;
}
public struct MaxManaShieldUpdateEventComponent
{
    public float newMaxManaShield;
}