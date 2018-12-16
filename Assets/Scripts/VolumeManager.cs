using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] public AudioMixer _audio;

    public void SetVolume(float volume)
    {
        _audio.SetFloat("volume", volume);
    }
}
