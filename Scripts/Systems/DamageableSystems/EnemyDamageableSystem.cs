using Leopotam.Ecs;
using UnityEngine;

public class EnemyDamageableSystem : IEcsRunSystem
{
    private EcsWorld _world;
    private EcsFilter<DefenceComponent, EnemyComponent, OnTriggerEnterComponent> _filter = null;

    public void Run()
    {
        foreach (var i in _filter)
        {
            ref var OnTriggerEnterComponent = ref _filter.Get3(i);
            ref var defenceComponent = ref _filter.Get1(i);

            if ((OnTriggerEnterComponent.first != null) && 
                (OnTriggerEnterComponent.other != null))
            { 
                if (OnTriggerEnterComponent.first.CompareTag(GlobalEnvironment.enemyTag) && OnTriggerEnterComponent.other.CompareTag(GlobalEnvironment.playerAttackTag)) 
                {
                    Bullet bullet = OnTriggerEnterComponent.other.GetComponent<Bullet>();
                    BulletComponent bulletComponent = bullet.entity.Get<BulletComponent>();

                    float damage = bulletComponent.attackType.CalculateDamage(defenceComponent, bulletComponent.damage);
                    defenceComponent.hp -= damage;

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
