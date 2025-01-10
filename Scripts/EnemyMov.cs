using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    LevelManager manager;

    //PLAYER DETECT
    public GameObject playerTank;
    public Transform playerTransform;
    public string playerTag = "Player";

    public bool playerIn;

    //DISPARO
    public GameObject bulletPrefab;
    public Transform cabinTransform;
    public Transform cannonTransform;

    public float fireRate = 2f; // Velocidad de disparo en segundos
    private float nextFireTime = 0f;

    // NAV MESH
    private NavMeshAgent agent;

    //PATRULLA
    public Transform[] patrolPoints;
    public float moveSpeed = 5f;
    private Transform currentPatrolPoint;


    private void Start()
    {
        manager = FindObjectOfType<LevelManager>();

        SetRandomPatrolPoint();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Compara el tag del Player y pone la variable true
        if (other.CompareTag("Player"))
        {
            playerIn = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir el player lo pone en falso
        if (other.CompareTag("Player"))
        {
            playerIn = false;

        }
    }

    void Shoot()
    {
        // Instancia el proyectil en la posicion del canon
        Instantiate(bulletPrefab, cannonTransform.position, cannonTransform.rotation);
    }

    private void Update()
    {

        // PLAYER DECTECT ----------------------------------------------------------------------------------
        if (playerIn == true)
        {
            agent.destination = playerTransform.position;

            // Calcula la direccion hacia el jugador
            Vector3 directionToPlayer = playerTank.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            // Rota el canon hacia el jugador
            cabinTransform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);

            // Dispara si es hora de hacerlo
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }



        }
        else
        {
            // PATRULLA ----------------------------------------------------------------------------------------

            // Verifica si hay un punto de patrulla asignado
            if (currentPatrolPoint != null)
            {
                // Calcula la direcci�n hacia el punto de patrulla actual
                Vector3 direction = currentPatrolPoint.position - transform.position;

                // Mueve el tanque hacia el punto de patrulla actual
                transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

                // Mira hacia el punto de patrulla actual
                transform.LookAt(currentPatrolPoint);

                // Verifica si el tanque ha llegado al punto de patrulla actual
                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.2f)
                {
                    // Establece el siguiente punto de patrulla aleatorio
                    SetRandomPatrolPoint();
                }
            }

        
        }

        
    }

    void SetRandomPatrolPoint()
    {
        // Elije un punto de patrulla aleatorio
        if (patrolPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, patrolPoints.Length);
            currentPatrolPoint = patrolPoints[randomIndex];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            manager.enemyKills = manager.enemyKills + 1;
            // Si el tanque es golpeado por una bala, destruir el tanque
            Destroy(gameObject);
        }
    }

}
