using UnityEngine;

/// <summary>
/// This script is an example of sound manager
/// Multiple audio clips can be attache to the manager,
/// audioSource is Only one, pay attention that each sound is played using their own function.
/// </summary>


public class SoundManager : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip landSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayLandSound()
    {
        audioSource.PlayOneShot(landSound);
    }
}