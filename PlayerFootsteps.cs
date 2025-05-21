using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip pasoMetalico; // arrastrar el sonido aquí
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Este es el que se llama desde la animación
    public void ReproducirPaso()
    {
        if (pasoMetalico != null)
        {
            // Varía un poco el sonido para que no se sienta repetitivo
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.PlayOneShot(pasoMetalico);
        }
    }
}
