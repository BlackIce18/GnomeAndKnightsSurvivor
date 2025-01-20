using Leopotam.Ecs;
using UnityEngine;

public class EnemyDamageableSystem : IEcsRunSystem
{
    private EcsWorld _world;
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
                if (OnTriggerEnterComponent.first.CompareTag("Enemy") && OnTriggerEnterComponent.other.CompareTag("Bullet")) 
                {
                    Bullet bullet = OnTriggerEnterComponent.other.GetComponent<Bullet>();
                    BulletComponent bulletComponent = bullet.entity.Get<BulletComponent>();

                    float damage = bulletComponent.attackType.CalculateDamage(defenceComponent, bulletComponent.damage);
                    defenceComponent.hp -= damage;

                    Debug.Log("Create");
                    var textEntity = _world.NewEntity();
                    ref var damageTextComponent = ref textEntity.Get<DamageTextComponent>();
                    damageTextComponent.text = damage.ToString();
                    damageTextComponent.entity = textEntity;
                    damageTextComponent.position = bullet.transform.position;

                    bullet.AfterCollide();
                }
            }
        }
    }
}
