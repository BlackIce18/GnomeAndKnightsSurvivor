public struct PlayerHealthComponent
{
    public int currentHealthPoints;
    public int maxHealthPoints;
}

public struct PlayerHealthUpdateEventComponent
{
    public int newHealthPoints;
}
public struct PlayerMaxHealthUpdateEventComponent
{
    public int newMaxHealthPoints;
}
public struct PlayerManaShieldComponent
{
    public int currentManaShield;
    public int maxManaShield;
}

public struct PlayerManaShieldUpdateEventComponent
{
    public int newManaShield;
}
public struct PlayerMaxManaShieldUpdateEventComponent
{
    public int newMaxManaShield;
}