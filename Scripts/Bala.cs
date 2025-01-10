using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject efectoParticulas;

    public float velocidad = 10f;
    public int rebotesMaximos = 2;
    public float tiempoDeVida = 5f;

    private int rebotes = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        // Destruir la bala después de que pase el tiempo de vida
        tiempoDeVida -= Time.deltaTime;
        if (tiempoDeVida <= 0f)
        {
            Explotar();
        }
    }

    public void Disparar()
    {
        rebotes = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        rebotes++;

        if (rebotes >= rebotesMaximos)
        {
            Explotar();
        }
        else
        {
            Vector3 direccionReflejada = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(direccionReflejada);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("PlayerTank"))
        {
            Explotar();
        }
    }

    void Explotar()
    {
        // Instanciar el efecto de partículas en la posición de la bala
        Instantiate(efectoParticulas, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
