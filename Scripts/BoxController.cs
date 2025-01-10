using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject objetoAActivar; // El objeto que se activar� cuando la caja explote

    void Start()
    {
        // No necesitamos hacer nada aqu� con el prefab
    }

    public void Explotar()
    {
        // Desactivar el objeto actual
        gameObject.SetActive(false);
        objetoAActivar.SetActive(true);

        // Instanciar el prefab en la ubicaci�n actual del objeto que ejecuta el c�digo
        
        Instantiate(objetoAActivar, transform.position, transform.rotation);
        
    }
}


