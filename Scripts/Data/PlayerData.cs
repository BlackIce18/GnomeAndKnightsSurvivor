using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player", order = 1)]
public class PlayerData : ScriptableObject
{
    public int startHp;
    public float playerSpeed;
    public int startManaShield;
    public int startArmor;
    public int startHpRegen;
    public int startManaShieldRegen;
}
