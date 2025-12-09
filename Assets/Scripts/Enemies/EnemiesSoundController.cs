using UnityEngine;

public class EnemiesSoundController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip sonidoDisparoEnemigo;
    public AudioClip sonidoMuerteEnemigo;
    public AudioClip sonidoAlertaEnemigo;
    public AudioClip sonidoGolpeEnemigo;

    public void playDisparoEnemigo()
    {
        PlayOneShotIfNotNull(sonidoDisparoEnemigo);
    }

    public void playMuerteEnemigo()
    {
        PlayOneShotIfNotNull(sonidoMuerteEnemigo);
    }

    public void playGolpeEnemigo()
    {
        PlayOneShotIfNotNull(sonidoGolpeEnemigo);
    }

    public void StartAlertaEnemigo()
    {
        if (audioSource == null || sonidoAlertaEnemigo == null)
            return;

        if (audioSource.isPlaying && audioSource.clip == sonidoAlertaEnemigo)
            return;

        audioSource.clip = sonidoAlertaEnemigo;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopAlertaEnemigo()
    {
        if (audioSource == null) return;

        if (audioSource.clip == sonidoAlertaEnemigo)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }

    private void PlayOneShotIfNotNull(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}