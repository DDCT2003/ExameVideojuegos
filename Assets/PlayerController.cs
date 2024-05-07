using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public float jumpForce = 10f;   // Fuerza de salto del jugador
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 originalGravity;
    private bool JustJump = false;
    private float origianlMoveSpeed;
    private Vector3 OriginalPosition;
    public Camera miCamara; // Referencia a la cámara
    public Canvas perdisteText; // Referencia al texto de UI que mostrará el mensaje "Perdiste"


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        origianlMoveSpeed = moveSpeed;
        OriginalPosition = this.transform.position;
        Physics.gravity = originalGravity * 2f; // Duplicar la gravedad

    }

    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        //   rb.MovePosition(transform.position + movement);

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            JustJump = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && JustJump)
        {  // Doble salto

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            JustJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        { //correr
            moveSpeed *= 2;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        { //retornar a velocidad normal

            moveSpeed = origianlMoveSpeed;
        }

        if (!isGrounded && rb.velocity.y < 0)
        {

            Physics.gravity = originalGravity * 3f; // Aumentar la gravedad
        }


        if (this.transform.position.y < -6)
        {

            this.transform.position = OriginalPosition; //volver a posicion original
        }
    }
   
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        // Verificar si la colisión es con una bala
        if (collision.gameObject.name == "Enemigo")
        {
            this.gameObject.SetActive(false);
            miCamara.gameObject.SetActive(true);
            perdisteText.gameObject.SetActive(true);
            Debug.Log("Perdiste");

        }
    }
}