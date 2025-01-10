using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TankMov : MonoBehaviour
{
    DontDestroy dontDestroy;
    LevelManager levelManager;
    ExplosionClip explosionClip;

    public Animator animator;

    public Transform Transform;

    public GameObject balaPrefab; // Prefab de la bala
    public Transform puntoDisparo; // Punto de origen del disparo
    public GameObject minaPrefab; // Prefab de la mina
    public GameObject ExplosionEffect; // Prefab del efecto de explosión

    private CharacterController controller;

    public string sceneName;

    // floats
    public float speed = 5f; // Velocidad del tanque
    public float fireRate = 1f; // Cadencia de disparo (bala por segundo)
    private float nextFireTime = 0f; // Tiempo hasta el próximo disparo permitido

    // Minas
    public int maxMinas = 3; // Máximo número de minas
    public int currentMinas; // Número actual de minas colocadas

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentMinas = maxMinas; // Inicializamos el número de minas al máximo permitido
        levelManager = FindObjectOfType<LevelManager>(); // Obtener referencia al LevelManager en la escena
        explosionClip = FindObjectOfType<ExplosionClip>(); // Obtener referencia al ExplosionClip en la escena
    }

    private void Update()
    {
        // Detectar el clic izquierdo del ratón para disparar
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            Disparar();
        }

        // Detectar la tecla 'Q' para soltar una mina
        if (Input.GetKeyDown(KeyCode.Q) && currentMinas > 0)
        {
            SoltarMina();
        }
    }

    void FixedUpdate()
    {
        // MOVIMIENTO -----------------------------------------------------------------------------------------------------------------

        // Obtener la entrada del teclado
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular la dirección del movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);

        // Aplicar el movimiento al objeto
        controller.Move(movimiento * speed * Time.deltaTime);

        // Actualizar los parámetros del Animator
        if (movimiento != Vector3.zero)
        {
            animator.SetFloat("moveX", movimiento.x);
            animator.SetFloat("moveY", movimiento.z); // Usamos movimiento.z ya que es el eje vertical en un top-down
        }
    }

    // VOIDS -----------------------------------------------------------------------------------------------------------------------

    void Disparar()
    {
        // Instanciar una nueva bala
        GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);

        // Obtener el componente de la bala y establecer su velocidad inicial
        Bala bala = nuevaBala.GetComponent<Bala>();
        if (bala != null)
        {
            bala.Disparar();
        }

        // Actualizar el tiempo para el próximo disparo
        nextFireTime = Time.time + 1f / fireRate;
    }

    void SoltarMina()
    {
        // Instanciar una nueva mina en la posición actual del tanque
        Instantiate(minaPrefab, Transform.position, transform.rotation);

        // Reducir el número de minas disponibles
        currentMinas = currentMinas - 1;
        levelManager.RestarMina();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Instanciar el efecto de explosión
            Instantiate(ExplosionEffect, Transform.position, transform.rotation);
            Debug.Log("Explosion");

            // Reproducir el sonido de la explosión
            explosionClip.PlayExplosionSound();

            // Llamar al método OnPlayerDestroyed del LevelManager para iniciar el fundido
            levelManager.OnPlayerDestroyed();

            // Destruir el tanque inmediatamente
            Destroy(gameObject);
        }
    }
}
