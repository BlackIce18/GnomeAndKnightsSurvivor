using UnityEngine;
public struct BulletComponent
{
    public int damage;
    public float maxLifeTime;
    public float speed;
    public float size;
    public float maxSize;
    // ��� ������ ������������ ��������
    public GameObject instance;

    //StartPosition ������� ������
    //����� ������ ����������� ���������� ����� � endPosition
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Vector3 direction;

    //����� ������ ������������� �������� (���� != maxLifeTime)
    public float currentLifeTime;
    public ObjectPool<BulletComponent> belongsToPool;
    public IAttackType attackType;
    public void Default()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
        direction = Vector3.zero;
        currentLifeTime = 0;
        if (instance != null)
        {
            instance.SetActive(false);
        }
    }
}