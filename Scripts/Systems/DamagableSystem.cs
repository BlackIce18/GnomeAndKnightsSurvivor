using Leopotam.Ecs;
using UnityEngine;

public class DamagableSystem : IEcsRunSystem
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
                if(OnTriggerEnterComponent.first.CompareTag("Enemy") &&
                   OnTriggerEnterComponent.other.CompareTag("Bullet")) 
                {
                    Debug.Log(defenceComponent.hp);
                    Bullet bullet = OnTriggerEnterComponent.other.GetComponent<Bullet>();
                    EcsEntity bulletEntity = bullet.entity;
                    defenceComponent.hp -= bulletEntity.Get<BulletComponent>().damage;
                    bullet.gameObject.SetActive(false);
                    //Bullet вернуть в пул bulletpool

                    if (defenceComponent.hp <= 0)
                    {
                        //”брать enemy в пул
                        //_filter.GetEntity(i)

                    }
                }
                _filter.GetEntity(i).Get<OnTriggerEnterComponent>().first = null;
                _filter.GetEntity(i).Get<OnTriggerEnterComponent>().other = null;
            }

        }
    }
}
