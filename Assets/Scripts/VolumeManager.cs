using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{

    [SerializeField] public AudioMixer audio;

    // Update is called once per frame
    public void SetVolume(float volume)
    {
        audio.SetFloat("volume", volume);
    }
}
