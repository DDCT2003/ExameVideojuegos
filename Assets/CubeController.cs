using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpInterval = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Jump", jumpInterval, jumpInterval);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con una bala
        if (collision.gameObject.name == "Bala(Clone)")
        {
            // Destruir el objeto
            Destroy(gameObject);
        }
    }
}

