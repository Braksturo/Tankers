using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CabinaController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Transform tarjet;

    // Apuntar y disparar al jugador
    

    public void Disparar()
    {
        // Instanciar una nueva bala // GameObject nuevaBala = 
        GameObject nuevaBala = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                
    }

    public void DispararRebote(Vector3 direccionRebote)
    {
        GameObject bala = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(direccionRebote));
        bala.GetComponent<Rigidbody>().velocity = direccionRebote * 20; // Velocidad de la bala
    }
}


