using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource playerAudioSource;
    public AudioClip walkClipSFX;
    public AudioClip jumpClipSFX;
    public AudioClip throwClipSFX;
    public AudioClip landClipSFX;

    private void Awake()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    public void PlayWalk()
    {
        playerAudioSource.clip = walkClipSFX;
        playerAudioSource.Play();
    }

    public void PlayJump()
    {
        playerAudioSource.clip = jumpClipSFX;
        playerAudioSource.Play();
    }
    public void PlayThrow()
    {
        playerAudioSource.clip = throwClipSFX;
        playerAudioSource.Play();
    }
    public void PlayLand()
    {
        playerAudioSource.clip = landClipSFX;
        playerAudioSource.Play();
    }
}
