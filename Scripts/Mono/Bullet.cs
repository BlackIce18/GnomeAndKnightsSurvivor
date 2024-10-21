using Leopotam.Ecs;
using UnityEngine;
public interface IBulletAfterCollide
{
    public void AfterCollide();
}
public class Bullet : MonoBehaviour, IBulletAfterCollide    
{
    public EcsEntity entity;

    public void AfterCollide()
    {
        Destroy(entity.Get<BulletComponent>());
    }

    private void Destroy(BulletComponent bulletComponent)
    {
        bulletComponent.Default();
        bulletComponent.belongsToPool.AddToPool(bulletComponent);
    }
}
