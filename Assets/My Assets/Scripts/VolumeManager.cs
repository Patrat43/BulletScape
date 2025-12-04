using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    // The exposed parameter you want this slider to control
    public string parameterName = "MasterVolume";

    void Start()
    {
        // Load the current mixer value
        float dB;
        if (mixer.GetFloat(parameterName, out dB))
        {
            slider.value = Mathf.Pow(10, dB / 20f);  // convert dB to linear
        }

        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        // Convert slider (0–1) to decibels
        float dB = Mathf.Log10(value) * 20f;

        mixer.SetFloat(parameterName, dB);
    }
}
