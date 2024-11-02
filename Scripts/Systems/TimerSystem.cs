using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class TimerSystem : IEcsInitSystem, IEcsRunSystem
{
    private SceneData _sceneData;
    private EcsFilter<TimerComponent> _filter;
    private float _Timer = 1f;
    private float _elapsedTime = 0;

    public void Init()
    {
        /*TimerCallback tm = new TimerCallback(ChangeTimer);
        Timer timer = new Timer(tm, null, 0, 1000);*/
        float maxSecondsTime = (15 * 60) + 1;
        _sceneData.timerSlider.maxValue = maxSecondsTime;
    }

    public void Run()
    {
        if (((_elapsedTime += Time.deltaTime) >= _Timer) && (_sceneData.timerSlider.value + 1 != _sceneData.timerSlider.maxValue))
        {
            ChangeTimer();
            _elapsedTime = 0;

            _sceneData.timerSlider.value += 1;
            ref TimerComponent timerComponent = ref _filter.Get1(0);
            _sceneData.timerText.text = ZeroBeforeDigit(timerComponent.minutes) + ":" + ZeroBeforeDigit(timerComponent.seconds);
        }
    }

    private void ChangeTimer()
    {
        ref TimerComponent timerComponent = ref _filter.Get1(0);
        timerComponent.seconds += 1;
        if(timerComponent.seconds == 60)
        {
            timerComponent.seconds = 0;
            timerComponent.minutes += 1;
        }
        if(timerComponent.minutes == 60)
        {
            timerComponent.minutes = 0;
            timerComponent.hours += 1;
        }
        if(timerComponent.hours == 24)
        {
            timerComponent.hours = 0;
        }
    }

    private string ZeroBeforeDigit(float time)
    {
        return time <= 9 ? "0" + time.ToString() : time.ToString();
    }
}
