using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Bullet", order = 1)]
public class BulletData : ScriptableObject
{
    public float damage;
    public float lifeTime;
    public float speed;
    public float size;
}
