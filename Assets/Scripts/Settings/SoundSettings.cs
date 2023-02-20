using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer m_masterMixer;

    public void SetSFXVolume(Slider volume) =>
        m_masterMixer.SetFloat("SFX", volume.value);

    public void SetMusicVolume(Slider volume) =>
        m_masterMixer.SetFloat("Music", volume.value);

    public void SetMasterVolume(Slider volume) =>
        m_masterMixer.SetFloat("Master", volume.value);
}