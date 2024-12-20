using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RarityAndColor
{
    public Color32 color;
    public ShopItemRarity rarity;
}
[CreateAssetMenu(menuName = "ScriptableObjects/Shop/BorderColors", order = 1)]
public class BorderColorsData : ScriptableObject
{
    [SerializeField] private List<RarityAndColor> borderColor;

    public List<RarityAndColor> BorderColors { private set { borderColor = value; } get { return borderColor; } }
}
