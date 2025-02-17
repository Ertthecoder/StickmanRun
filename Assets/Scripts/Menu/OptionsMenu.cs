using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer, audioMixer1;
    public void SetVFX(float volume)
    {
       audioMixer.SetFloat("vfxVolume", volume);
    }
    public void SetMusic(float volume)
    {
        audioMixer1.SetFloat("musicVolume", volume);
    }
}
