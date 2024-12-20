using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop/ResetShopData", order = 1)]
public class ResetShopData : ScriptableObject
{
    [SerializeField] private int _freeStartResetsCount = 5;
    [SerializeField] private int _freeBuyTime = 10;
    [SerializeField] private int _resetPriceIncrease = 100;
    [SerializeField] private float _resetTimeSeconds = 60;
    public int FreeStartResetsCount { get { return _freeStartResetsCount; } }
    public int FreeBuyTime { get {  return _freeBuyTime; } }
    public int ResetPriceIncrease { get { return _resetPriceIncrease; } }
    public float ResetTimeSeconds { get { return _resetTimeSeconds; } }
}
