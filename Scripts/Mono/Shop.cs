using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemData> _itemsData = new List<ShopItemData>();
    [SerializeField] private List<ShopUIButton> _shopButtons;
    [SerializeField] private ShopUIButton _resetButton;
    [SerializeField] private Slider _timerSlider;

    public List<ShopItemData> ItemsData { get { return _itemsData; } }
    public List<ShopUIButton> ShopButtons { get {  return _shopButtons; } }
    public ShopUIButton ResetButton { get {  return _resetButton; } }
    public Slider TimerSlider { get {  return _timerSlider; } }

    public void ChangeImage(int index, Sprite newSprite)
    {
        _shopButtons[index].Image.sprite = newSprite;
    }

    public void ChangeTimer(float value)
    {
        TimerSlider.value = value;
    }
}
