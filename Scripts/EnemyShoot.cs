using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float radioDeteccion = 5.0f;
    public LayerMask capasJugador;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float delayDisparo = 1.0f;

    private GameObject jugador;
    private float tiempoUltimoDisparo = Mathf.NegativeInfinity;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Si el jugador está dentro del radio de detección
        if (Vector3.Distance(transform.position, jugador.transform.position) <= radioDeteccion)
        {
            // Comprobar si el jugador está a la vista
            if (PuedeVerJugador())
            {
                // Disparar con un retraso
                if (Time.time >= tiempoUltimoDisparo + delayDisparo)
                {
                    Disparar();
                    tiempoUltimoDisparo = Time.time;
                }
            }
        }
    }

    bool PuedeVerJugador()
    {
        // Implementa la lógica para verificar si el enemigo puede ver al jugador
        RaycastHit hit;
        if (Physics.Linecast(transform.position, jugador.transform.position, out hit, capasJugador))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
    }
}
