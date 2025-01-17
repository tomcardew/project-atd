using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private string tooltip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GameManager.OnWaveStart += HandleWaveEnd;
    }

    private void OnDisable()
    {
        GameManager.OnWaveStart -= HandleWaveEnd;
    }

    public void SetTooltip(string text)
    {
        tooltip = text;
    }

    public string GetTooltip()
    {
        return tooltip;
    }

    public void ClearTooltip()
    {
        tooltip = "";
    }

    private void HandleWaveEnd()
    {
        StartCoroutine(PlaySoundForDuration(Prefabs.GetSound(Prefabs.SoundType.Alert), 2.3f));
        var alert = Instantiate(
            Prefabs.GetPrefab(Prefabs.AlertType.WaveIsComing),
            Vector2.zero,
            Quaternion.identity
        );
        Destroy(alert, 2.3f);
    }

    private IEnumerator PlaySoundForDuration(AudioClip clip, float duration)
    {
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        audioSource.Stop();
        audioSource.loop = false;
    }
}
