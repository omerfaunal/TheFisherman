using UnityEngine;
using UnityEngine.UI; // Include the UI namespace

public class SliderColorChanger : MonoBehaviour
{
    public Slider slider; // Reference to the slider
    public Color goodColor = Color.green; // Color for the good value
    public Color badColor = Color.red; // Color for the bad value
    public Image fillImage; // Reference to the slider's fill image

    void Update()
    {
        if (slider && fillImage) // Check if references are not null
        {
            // Lerp the color based on the slider's current value
            fillImage.color = Color.Lerp(badColor, goodColor, slider.value / slider.maxValue);
        }
    }
}
