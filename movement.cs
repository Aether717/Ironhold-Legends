using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    public float fuerzaGolpe;
    public LayerMask FloorLayer;
    public int maxJumps;
    public AudioClip audioSalto;

    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private bool lookingtoRight = true;
    private int remainingJumps;
    private Animator animator;
    private bool puedeMoverse = true;
    private void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        remainingJumps = maxJumps;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessMovement();
        ProcessJump();
    }

    bool TouchingGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, FloorLayer);
        return raycastHit.collider != null;
    }
    void ProcessJump()
    {
        if (TouchingGround())
        {
            remainingJumps = maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            remainingJumps--;
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 0f);
            rigidbody.AddForce(Vector2.up * jumpforce, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(audioSalto);
        }

    }

    void ProcessMovement()
    {
        if (!puedeMoverse) return;

        //Logica de movimiento
        float inputMovement = Input.GetAxis("Horizontal");
        if (inputMovement != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        rigidbody.linearVelocity = new Vector2(inputMovement * speed, rigidbody.linearVelocity.y);
        OrientationGestion(inputMovement);
    }
    void OrientationGestion(float inputMovement)
    {
        //Si se cumple la condici�n
        if ((lookingtoRight == true && inputMovement < 0) || (lookingtoRight == false && inputMovement > 0))
        {
            //Ejecutar c�digo de volteado
            lookingtoRight = !lookingtoRight;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    public void AplicarGolpe()
    {
        puedeMoverse = false;

        Vector2 direccionGolpe;
        if (rigidbody.linearVelocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rigidbody.AddForce(direccionGolpe * fuerzaGolpe);
        StartCoroutine(EsperarYActivarmovimiento());
    }

    IEnumerator EsperarYActivarmovimiento()
    {
        yield return new WaitForSeconds(0.1f);
        while (!TouchingGround())
        {
            yield return null;
        }
        puedeMoverse = true;
    }
}

