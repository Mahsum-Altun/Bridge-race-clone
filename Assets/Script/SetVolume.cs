using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer sound;
    public AudioMixer music;

    public void SetLevelSound(float sliderValue)
    {
        sound.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
    }
    public void SetLevelMusic(float sliderValue)
    {
        music.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
