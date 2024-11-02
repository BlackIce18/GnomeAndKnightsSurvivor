using UnityEngine;
using UnityEngine.UI;

public class ShopUIButton : MonoBehaviour 
{
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    public Image Image {  get { return _image; } }
    public Button Button {  get { return _button; } }
}
