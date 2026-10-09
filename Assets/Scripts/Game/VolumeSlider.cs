using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string mixerFloatName;
    [SerializeField] GameObject exitButton;
    [SerializeField] InputActionReference cancelInput;
    Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(mixerFloatName))
            slider.value = PlayerPrefs.GetFloat(mixerFloatName);

        else
            slider.value = 1;
        mixer.SetFloat(mixerFloatName, Mathf.Log10(slider.value) * 20);

    }

    // Update is called once per frame
    void Update()
    {
        if (cancelInput.action.WasPerformedThisFrame()&&EventSystem.current.currentSelectedGameObject==gameObject)
        {
            EventSystem.current.SetSelectedGameObject(exitButton);
        }
    }
    public void ChangeVolume()
    {
        mixer.SetFloat(mixerFloatName, Mathf.Log10(slider.value) * 20);
        PlayerPrefs.SetFloat(mixerFloatName, slider.value);
    }
    
}
