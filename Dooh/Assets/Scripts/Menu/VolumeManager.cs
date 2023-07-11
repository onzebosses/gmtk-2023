using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    private Slider slider;

    private enum VolumeType
    {
        MASTER,
        MUSIC,
        AMBIENCE,
        SFX
    }

    [Header("Type")]

    [SerializeField]
    private VolumeType volumeType;

    //private void Start()
    //{
    //    slider = GetComponent<Slider>();
    //    SetVolume(slider.value);
    //}

    private void Start()
    {
        setVolume();
    }

    private void Awake()
    {
        slider = this.GetComponentInChildren<Slider>();
    }

    public void SetVolume(float value)
    {
        numberText.text = value.ToString();
    }

    private void Update()
    {
        setVolume();
    }

    public void OnSliderValueChanged()
        {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = slider.value / 100;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = slider.value / 100;
                break;
            case VolumeType.AMBIENCE:
                AudioManager.instance.ambienceVolume = slider.value / 100;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = slider.value / 100;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }
    private void setVolume()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                slider.value = AudioManager.instance.masterVolume * 100;
                break;
            case VolumeType.MUSIC:
                slider.value = AudioManager.instance.musicVolume * 100;
                break;
            case VolumeType.AMBIENCE:
                slider.value = AudioManager.instance.ambienceVolume * 100;
                break;
            case VolumeType.SFX:
                slider.value = AudioManager.instance.SFXVolume * 100;
                break;
            default:
                Debug.LogWarning("Volume Type not supported: " + volumeType);
                break;
        }
    }
}
