using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndManaShieldBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public Slider Slider { get { return slider; } }
    public TextMeshProUGUI Text { get {  return text; } }
}
