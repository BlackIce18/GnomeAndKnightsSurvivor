using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public float startHp;
    public float startSpeed;
    public float startManaShield;
    public float startArmor;
    public float startHpRegen;
    public float startManaShieldRegen;
    public float timeToStartHpRegenAfterTakeDamage;
    public float timeToStartManaShieldRegenAfterTakeDamage;
}
