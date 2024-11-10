using Leopotam.Ecs;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<ShopItemData> _itemsData = new List<ShopItemData>();
    [SerializeField] private List<ShopUIButton> _shopButtons;
    [SerializeField] private ShopUIButton _resetButton;
    [SerializeField] private Slider _timerSlider;
    [SerializeField] private TextMeshProUGUI _resetCurrentPriceText;
    [SerializeField] private ResetShopData _resetShopData;
    public List<ShopItemData> ItemsData { get { return _itemsData; } }
    public List<ShopUIButton> ShopButtons { get {  return _shopButtons; } }
    public ShopUIButton ResetButton { get {  return _resetButton; } }
    public Slider TimerSlider { get {  return _timerSlider; } }
    public TextMeshProUGUI ResetCurrentPriceText { get {  return _resetCurrentPriceText; } }

    public ResetShopData ResetShopData { get { return _resetShopData; } }
    public void ChangeImage(int index, Sprite newSprite)
    {
        _shopButtons[index].Image.sprite = newSprite;
    }

    public void ChangeTimer(float value)
    {
        TimerSlider.value = value;
    }

    public void AddOnClick(ShopUIButton shopUIButton, ICommand command)
    {
        shopUIButton.Button.onClick.AddListener(command.Execute);
    }
    
    public void UpdateResetPrice(float newPrice)
    {
        ResetCurrentPriceText.text = newPrice.ToString();
    }

    public void UpdateTimerSlider(float value)
    {
        TimerSlider.value = value;
    }
}
