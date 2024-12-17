using UnityEngine;
public enum ShopItemRarity
{
    common,
    uncommon,
    rare,
    mythic,
    legendary
}
public abstract class ShopItemData : ScriptableObject
{
    public string title;
    public string description;
    public Sprite icon;
    public int cost;
    public ShopItemRarity rarity;
}