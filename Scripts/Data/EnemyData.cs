using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/Knight", order = 1)]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public int goldForKill;
    public int Xp;
    public DefenceComponent defenceComponent;
    public EnemyAttackComponent attackComponent;
    public bool canFollow;
}
