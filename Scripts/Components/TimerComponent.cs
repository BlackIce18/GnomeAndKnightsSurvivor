using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
public struct TimerComponent
{
    [Range(0, 59)] public float seconds;
    [Range(0, 59)] public float minutes;
    [Range(0, 23)] public float hours;
}
