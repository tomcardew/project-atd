using UnityEngine;

public enum AudioSourceType
{
    Attack,
    Resources,
    Alerts,
    UI
}

public class SoundManager : MonoBehaviour
{
    public AudioSource attackAudioSource;
    public AudioSource resourcesAudioSource;
    public AudioSource alertsAudioSource;
    public AudioSource uiAudioSource;

    private void Awake()
    {
        attackAudioSource = gameObject.AddComponent<AudioSource>();
        resourcesAudioSource = gameObject.AddComponent<AudioSource>();
        alertsAudioSource = gameObject.AddComponent<AudioSource>();
        uiAudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Play(
        AudioClip clip,
        AudioSourceType audioSourceType,
        Vector3 position,
        float volume = 1f
    )
    {
        AudioSource audioSource = null;
        switch (audioSourceType)
        {
            case AudioSourceType.Attack:
                audioSource = attackAudioSource;
                break;
            case AudioSourceType.Resources:
                audioSource = resourcesAudioSource;
                break;
            case AudioSourceType.Alerts:
                audioSource = alertsAudioSource;
                break;
            case AudioSourceType.UI:
                audioSource = uiAudioSource;
                break;
        }

        Play(audioSource, clip, position, volume);
    }

    private void Play(AudioSource source, AudioClip clip, Vector3 position, float volume)
    {
        if (source.isPlaying)
        {
            return;
        }
        source.clip = clip;
        source.transform.position = position;
        source.volume = volume;
        source.Play();
    }
}
