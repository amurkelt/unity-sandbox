using UnityEngine;
using UnityEngine.UI;

public class GradientSliderControl : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Gradient gradient;

    private void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(UpdateColor);
        UpdateColor(slider.value);
    }

    private void UpdateColor(float value)
    {
        Color newColor = gradient.Evaluate(slider.normalizedValue);
        fillImage.color = newColor;
    }
}