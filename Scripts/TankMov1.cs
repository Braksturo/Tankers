using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankMov1 : MonoBehaviour
{

    public string sceneName;

    // floats
    public float speed = 5f; // Velocidad del tanque

    void Update()
    {
        // MOVIMIENTO -----------------------------------------------------------------------------------------------------------------

        // Obtener la entrada del teclado
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento
        Vector3 movimiento = new Vector3(0f, movimientoVertical, movimientoHorizontal) * speed * Time.deltaTime;

        // Aplicar el movimiento al objeto
        transform.Translate(movimiento);

             

    }

    // VOIDS -----------------------------------------------------------------------------------------------------------------------


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            SceneManager.LoadScene(sceneName);

            // Si el tanque es golpeado por una bala, destruir el tanque
            Destroy(gameObject);
        }
    }
}
