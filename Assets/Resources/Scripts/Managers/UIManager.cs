using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private string tooltip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        GameManager.OnWaveStart += HandleWaveStart;
    }

    private void OnDisable()
    {
        GameManager.OnWaveStart -= HandleWaveStart;
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

    public void ShowFirstDrawModal(Card[] cards)
    {
        Manager.Time.isPaused = true;
        var modal = Instantiate(
            Prefabs.GetPrefab(Prefabs.Modals.FirstDraw),
            Vector2.zero,
            Quaternion.identity
        );
        FirstDrawModalController ctrl = modal.GetComponent<FirstDrawModalController>();
        ctrl.cards = cards;
    }

    public void ShowAddCardModal(Card[] cards)
    {
        Manager.Time.isPaused = true;
        var modal = Instantiate(
            Prefabs.GetPrefab(Prefabs.Modals.AddCard),
            Vector2.zero,
            Quaternion.identity
        );
        AddCardModalController ctrl = modal.GetComponent<AddCardModalController>();
        ctrl.cards = cards;
    }

    private void HandleWaveStart()
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
