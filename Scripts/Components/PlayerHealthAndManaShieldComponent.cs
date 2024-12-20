public struct PlayerHealthComponent
{
    public int currentHealth;
    public int maxHealth;
}

public struct PlayerHealthUpdateEventComponent
{
    public int newHealth;
}
public struct PlayerMaxHealthUpdateEventComponent
{
    public int newMaxHealth;
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