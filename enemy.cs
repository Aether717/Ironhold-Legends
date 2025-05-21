using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2f;
    public float cooldownAtaque = 1f;

    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    private Transform jugador;

    private int golpesRecibidos = 0;

    void Start()
    {
        // Obtenemos el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Buscamos al jugador por su etiqueta
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Player'");
        }
    }

    void Update()
    {
        // Movimiento hacia el jugador
        if (jugador != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.position += (Vector3)(direccion * velocidadMovimiento * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si el enemigo colisiona con el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            // Realizamos el ataque
            puedeAtacar = false;
            CambiarColorTransparente();

            // Llamamos al GameManager para que pierda vida
            GameManager.Instance.PerderVida();

            // Aplicamos el golpe al jugador
            other.gameObject.GetComponent<Movement>().AplicarGolpe();

            // Reactivamos el ataque después de un tiempo
            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        // Reactivamos el ataque y restauramos el color original
        puedeAtacar = true;
        CambiarColorOpaco();
    }

    void CambiarColorTransparente()
    {
        // Cambiamos el color del sprite para indicar que no puede atacar
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
    }

    void CambiarColorOpaco()
    {
        // Restauramos el color original
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
    }

    public void RecibirGolpe()
    {
        golpesRecibidos++;

        if (golpesRecibidos >= 2)
        {
            // Destruimos el enemigo si recibe suficiente daño
            Destroy(gameObject);
        }
    }
}
