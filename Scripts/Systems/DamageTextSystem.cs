using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct DamageTextStruct
{
    public string text;
    public Vector3 position;
}
public struct DamageTextRemoveTimeStruct : IEcsIgnoreInFilter 
{
    public GameObject textObject;
    public EcsEntity entity;
    public float startTime;
    public float endTime;
}
public struct ActiveDamageText
{
    public List<DamageTextRemoveTimeStruct> damageTexts;
}
public class DamageTextSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsWorld _world;
    private EcsFilter<OnTriggerEnterComponent, DefenceComponent> _filter = null;
    private EcsFilter<DamageTextStruct, DamageTextRemoveTimeStruct> _removeTextFilter = null;
    private EcsFilter<ActiveDamageText> _activeDamageTextFilter = null;
    private ActiveDamageText _activeDamageText;
    public void Init()
    {
        _activeDamageText = _activeDamageTextFilter.Get1(0);
    }

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

                    var textEntity = _world.NewEntity();
                    GameObject instance = GameObject.Instantiate(_sceneData.damageTextPrefab, _sceneData.damageTextParent.transform);
                    DamageText damageText = instance.GetComponent<DamageText>();
                    damageText.Text.text = bulletComponent.damage.ToString();

                    ref var damageTextRemoveTime = ref textEntity.Get<DamageTextRemoveTimeStruct>();
                    damageTextRemoveTime.entity = textEntity;
                    damageTextRemoveTime.textObject = instance;
                    damageTextRemoveTime.startTime = Time.time;
                    damageTextRemoveTime.endTime = Time.time + damageText.AnimationClipDuration;

                    ref var damageTextStruct = ref textEntity.Get<DamageTextStruct>();
                    damageTextStruct.text = bulletComponent.damage.ToString();
                    damageTextStruct.position = OnTriggerEnterComponent.first.gameObject.transform.position;
                    instance.transform.position = new Vector3(damageTextStruct.position.x, instance.transform.position.y, damageTextStruct.position.z);
                    damageText.PlayAnimation();
                    _activeDamageText.damageTexts.Add(damageTextRemoveTime);
                    
                }
            }
        }

        for(int i = 0; i < _activeDamageText.damageTexts.Count; i++)
        {
            if (Time.time >= _activeDamageText.damageTexts[i].endTime)
            {
                GameObject.Destroy(_activeDamageText.damageTexts[i].textObject);
                _activeDamageText.damageTexts[i].entity.Del<DamageTextStruct>();
                _activeDamageText.damageTexts[i].entity.Del<DamageTextRemoveTimeStruct>();
                _activeDamageText.damageTexts.RemoveAt(i);
            }
        }
    }
}
