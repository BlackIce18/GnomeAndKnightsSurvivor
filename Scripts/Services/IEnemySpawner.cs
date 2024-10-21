using System.Numerics;

public interface IEnemySpawner
{
    public void CreateNewEnemy(UnityEngine.Vector3 position, EnemyData enemyData);
    public EnemyComponent GetFromPool(UnityEngine.Vector3 position);
}