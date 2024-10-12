using UnityEngine;
public struct BulletComponent
{
    public int damage;
    public float maxLifeTime;
    public float speed;
    public float size;
    public float maxSize;
    // ѕри спавне записываетс€ значение
    public GameObject instance;

    //StartPosition позици€ игрока
    //ѕосле спавна вычисл€етс€ координаты мышки в endPosition
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 direction;

    //ѕосле спавна увеличиваетс€ значение (пока != maxLifeTime)
    public float currentLifeTime;
    public ObjectPool<BulletComponent> belongsToPool;

    public void Default()
    {
        this.startPosition = Vector3.zero;
        this.endPosition = Vector3.zero;
        this.direction = Vector3.zero;
        this.currentLifeTime = 0;
        this.instance.SetActive(false);
    }
}