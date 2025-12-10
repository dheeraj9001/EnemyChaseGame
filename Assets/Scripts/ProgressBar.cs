using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0f;
    }

    public void SetProgress(float value)
    {
        slider.value = value;
    }
}
