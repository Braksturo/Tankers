using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinController : MonoBehaviour
{
    
    public LevelManager levelManager;
    
    public float elevationAmount = 2f; // Cantidad total de elevaci�n en Y
    public float cycleDuration = 0.2f; // Duraci�n del ciclo en segundos (20 ms)
    private Vector3 initialPosition; // Posici�n inicial de la moneda
    private bool goingUp = true; // Bandera para determinar la direcci�n del movimiento

    private void Start()
    {
        initialPosition = transform.position; // Guardar la posici�n inicial

       
    }

    private void Update()
    {
        float cycleTime = Mathf.PingPong(Time.time / cycleDuration, 1.0f);
        float elevation = Mathf.Lerp(0, elevationAmount, cycleTime);

        if (goingUp)
        {
            transform.position = initialPosition + new Vector3(0, elevation, 0);
        }
        else
        {
            transform.position = initialPosition + new Vector3(0, elevationAmount - elevation, 0);
        }

        if (cycleTime >= 1.0f)
        {
            goingUp = !goingUp;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerTank"))
        {
            
            Destroy(gameObject);
            levelManager.SumarCoins();
                      

        }
    }
}

