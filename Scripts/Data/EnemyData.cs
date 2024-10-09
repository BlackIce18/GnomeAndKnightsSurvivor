using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Enemy/Knight", order = 1)]
public class EnemyData : ScriptableObject
{
    public GameObject prefab;
    public float speed;
    public int goldForKill;
    public DefenceComponent defenceComponent;
    public AttackComponent attackComponent;
}
