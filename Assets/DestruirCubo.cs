using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirCubo : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisi�n es con una bala
        if (collision.gameObject.CompareTag("Bala(Clone)"))
        {
            // Destruir el objeto
            Destroy(gameObject);
        }
    }
}
