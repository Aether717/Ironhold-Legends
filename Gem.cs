using UnityEngine;

public class Gem : MonoBehaviour
{

    public int valor = 1;
    public AudioClip audioGema;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.sumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(audioGema);
        }

    }

}
