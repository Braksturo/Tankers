using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject objetoAActivar; // El objeto que se activará cuando la caja explote

    void Start()
    {
        // No necesitamos hacer nada aquí con el prefab
    }

    public void Explotar()
    {
        // Desactivar el objeto actual
        gameObject.SetActive(false);
        objetoAActivar.SetActive(true);

        // Instanciar el prefab en la ubicación actual del objeto que ejecuta el código
        
        Instantiate(objetoAActivar, transform.position, transform.rotation);
        
    }
}


