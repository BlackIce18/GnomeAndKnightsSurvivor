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
    public void Init()
    {
        TimerCallback tm = new TimerCallback(ChangeTimer);
        Timer timer = new Timer(tm, null, 0, 1000);
    }

    public void Run()
    {
        ref TimerComponent timerComponent = ref _filter.Get1(0);
        _sceneData.timerText.text = ZeroBeforeDigit(timerComponent.minutes) + ":" + ZeroBeforeDigit(timerComponent.seconds);
    }

    private void ChangeTimer(object obj)
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
