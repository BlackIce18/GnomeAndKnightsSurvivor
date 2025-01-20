using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct DamageTextComponent
{
    public string text;
    public EcsEntity entity;
    public Vector3 position;
}
public struct DamageTextRemoveTimeStruct : IEcsIgnoreInFilter 
{
    public GameObject textObject;
    public EcsEntity entity;
    public float startTime;
    public float endTime;
}
public struct AllActiveDamageTextComponent
{
    public List<DamageTextRemoveTimeStruct> damageTexts;
}
public class DamageTextSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world;
    private EcsFilter<OnTriggerEnterComponent, DefenceComponent> _filter = null;
    private EcsFilter<DamageTextComponent, DamageTextRemoveTimeStruct> _removeTextFilter = null;
    private EcsFilter<AllActiveDamageTextComponent> _activeDamageTextFilter = null;
    private AllActiveDamageTextComponent _activeDamageText;

    private EcsFilter<DamageTextComponent> _damageTextFilter = null;
    public void Init()
    {
        _activeDamageText = _activeDamageTextFilter.Get1(0);
    }

    public void Run()
    {

        foreach (var i in _damageTextFilter)
        {
            ref var damageTextComponent = ref _damageTextFilter.Get1(i);
            ref var entity = ref damageTextComponent.entity;
            

            ref var damageTextRemoveTime = ref entity.Get<DamageTextRemoveTimeStruct>();
            if (damageTextRemoveTime.textObject == null)
            {
                damageTextRemoveTime.entity = entity;
                damageTextRemoveTime.textObject = GameObject.Instantiate(_sceneData.damageTextPrefab, _sceneData.damageTextParent.transform);
                damageTextRemoveTime.startTime = Time.time;
                DamageText damageText = damageTextRemoveTime.textObject.GetComponent<DamageText>();
                damageTextRemoveTime.endTime = Time.time + damageText.AnimationClipDuration;


                damageText.Text.text = damageTextComponent.text;
                damageText.transform.position = damageTextComponent.position;
                damageTextRemoveTime.textObject.transform.position = new Vector3(damageTextComponent.position.x, damageTextRemoveTime.textObject.transform.position.y, damageTextComponent.position.z);
                damageText.PlayAnimation();
                _activeDamageText.damageTexts.Add(damageTextRemoveTime);
            }
        }



        
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
                }
            }
        }

        for(int i = 0; i < _activeDamageText.damageTexts.Count; i++)
        {
            if (Time.time >= _activeDamageText.damageTexts[i].endTime)
            {
                GameObject.Destroy(_activeDamageText.damageTexts[i].textObject);
                _activeDamageText.damageTexts[i].entity.Del<DamageTextComponent>();
                _activeDamageText.damageTexts[i].entity.Del<DamageTextRemoveTimeStruct>();
                _activeDamageText.damageTexts.RemoveAt(i);
            }
        }
    }
}
