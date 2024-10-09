using System.Numerics;

public interface IEnemySpawner
{
    public void Spawn(UnityEngine.Vector3 position, EnemyData enemyData);
}