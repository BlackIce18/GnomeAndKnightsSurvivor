using Leopotam.Ecs;
using UnityEngine;

public class DamageableSystem : IEcsRunSystem
{
    private EcsFilter<OnTriggerEnterComponent, DefenceComponent> _filter = null;
    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var OnTriggerEnterComponent = ref _filter.Get1(i);
            ref var defenceComponent = ref _filter.Get2(i);

            if ((OnTriggerEnterComponent.first != null) && 
                (OnTriggerEnterComponent.other != null))
            { 
                if ((OnTriggerEnterComponent.first.CompareTag("Enemy") &&
                   OnTriggerEnterComponent.other.CompareTag("Bullet")) ||
                   (OnTriggerEnterComponent.first.CompareTag("Player") &&
                   OnTriggerEnterComponent.other.CompareTag("EnemyBullet"))) 
                {
                    Bullet bullet = OnTriggerEnterComponent.other.GetComponent<Bullet>();
                    BulletComponent bulletComponent = bullet.entity.Get<BulletComponent>();
                    defenceComponent.hp -= bulletComponent.attackType.CalculateDamage(defenceComponent, bulletComponent.damage);
                    Debug.Log(defenceComponent.hp);
                }
                _filter.GetEntity(i).Get<OnTriggerEnterComponent>().first = null;
                _filter.GetEntity(i).Get<OnTriggerEnterComponent>().other = null;
            }

        }
    }
}
