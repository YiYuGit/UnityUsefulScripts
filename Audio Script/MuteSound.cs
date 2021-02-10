using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script mute and unmute all sound in the scene when press 'm'
/// </summary>

public class MuteSound : MonoBehaviour
{
    [Header("Press 'm' to mute and unmute")]
    [SerializeField] private bool muted;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            ToggleAudio();
    }

    public void DisableAudio()
    {
        SetAudioMute(false);
    }

    public void EnableAudio()
    {
        SetAudioMute(true);
    }

    public void ToggleAudio()
    {
        if (muted)
            DisableAudio();
        else
            EnableAudio();
    }

    private void SetAudioMute(bool mute)
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = mute;
        }
        muted = mute;
    }
}