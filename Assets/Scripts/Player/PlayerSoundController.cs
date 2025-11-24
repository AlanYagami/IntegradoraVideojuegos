using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoDisparo;
    public AudioClip sonidoDisparoCargado;
    public AudioClip sonidoDash;
    public AudioClip sonidoPowerUp;
    public AudioClip sonidoRealentizado;

    public void playDisparo()
    {
        audioSource.PlayOneShot(sonidoDisparo);
    }

    public void playDisparoCargado()
    {
        audioSource.PlayOneShot(sonidoDisparoCargado);
    }

    public void playDash()
    {
        audioSource.PlayOneShot(sonidoDash);
    }

    public void playPowerUp()
    {
        audioSource.PlayOneShot(sonidoPowerUp);
    }

    public void playRealentizado()
    {
        audioSource.PlayOneShot(sonidoRealentizado);
    }
}
