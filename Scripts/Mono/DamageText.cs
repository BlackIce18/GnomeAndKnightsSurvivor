using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Animation _animation;
    public TextMeshProUGUI Text {  get { return _text; } }
    public float AnimationClipDuration { get { return _animation.clip.length; } }
    public void PlayAnimation() 
    {
        _animation.Play();
    }
}
