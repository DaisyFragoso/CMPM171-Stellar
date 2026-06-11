using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GreyscaleToggle : MonoBehaviour
{
    public Volume globalVolume;
    public Slider slider;

    private ColorAdjustments colorAdjustments;

    void Start()
    {
        globalVolume.profile.TryGet(out colorAdjustments);
        slider.onValueChanged.AddListener(OnSliderChanged);

        float savedValue = PlayerPrefs.GetFloat("greyscale", 0f);
        slider.value = savedValue;
        OnSliderChanged(savedValue);
    }

    public void OnSliderChanged(float value)
    {
        if (colorAdjustments != null)
            colorAdjustments.saturation.value = Mathf.Lerp(0f, -100f, value);

        PlayerPrefs.SetFloat("greyscale", value);
    }
}