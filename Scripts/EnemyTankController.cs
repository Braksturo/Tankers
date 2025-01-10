using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTankController : MonoBehaviour
{
    LevelManager manager;
    ExplosionClip ExplosionClip;

    public Animator ani;
    public GameObject target;
    public CabinaController cabinaController;
    public GameObject ExplosionEffect; // Prefab del efecto de explosión

    public int rutina;
    public float cronometro;
    public float grado;
    private Quaternion angulo;

    public float walkSpeed = 5;
    public float runSpeed = 10;
    public bool atacando;
    public float shootDelay = 1.0f; // Delay configurable desde el Inspector

    public NavMeshAgent agente;
    public float detectionRange = 100;
    public float shootRange = 75;

    private float nextShootTime; // Tiempo hasta el próximo disparo permitido

    // Definir los límites del área de patrullaje
    private Vector3 patrolAreaCenter;
    public float patrolAreaMinRadius = 10f; // Radio mínimo del área de patrullaje
    public float patrolAreaMaxRadius = 50f; // Radio máximo del área de patrullaje

    void Start()
    {
        manager = FindObjectOfType<LevelManager>(); // Encuentra el LevelManager en la escena
        ExplosionClip = FindObjectOfType<ExplosionClip>();
        ani = GetComponent<Animator>();
        target = GameObject.Find("PlayerTank");
        cabinaController = GetComponentInChildren<CabinaController>();
        agente = GetComponent<NavMeshAgent>();
        nextShootTime = 0f;

        // Guardar la posición inicial como el centro del área de patrullaje
        patrolAreaCenter = transform.position;
    }

    void Update()
    {
        Comportamiento_Enemigo();
        ApuntarCabina();
    }

    public void Comportamiento_Enemigo()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador > detectionRange)
        {
            Patrullar();
        }
        else
        {
            PerseguirYAtacar();
        }
    }

    void Patrullar()
    {
        agente.isStopped = false;
        ani.SetBool("run", false);
        cronometro += Time.deltaTime;
        if (cronometro >= 4)
        {
            rutina = Random.Range(0, 2);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
                ani.SetBool("walk", false);
                break;

            case 1:
                Vector3 randomDirection;
                float distance;
                do
                {
                    randomDirection = Random.insideUnitSphere * patrolAreaMaxRadius;
                    randomDirection += patrolAreaCenter;
                    distance = Vector3.Distance(patrolAreaCenter, randomDirection);
                } while (distance < patrolAreaMinRadius || distance > patrolAreaMaxRadius);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, patrolAreaMaxRadius, NavMesh.AllAreas))
                {
                    agente.SetDestination(hit.position);
                    agente.isStopped = false;
                    ani.SetBool("walk", true);
                }
                rutina++;
                break;
        }
    }

    void PerseguirYAtacar()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);
        agente.isStopped = false;

        if (distanciaAlJugador > shootRange)
        {
            ani.SetBool("walk", false);
            ani.SetBool("run", true);
            ani.SetBool("attack", false);
            agente.SetDestination(target.transform.position);
        }
        else
        {
            if (HayLineaDeVision() && Time.time >= nextShootTime)
            {
                StartCoroutine(ApuntarYDisparar());
                if (Time.time >= nextShootTime)
                {
                    cabinaController.Disparar();
                    Debug.Log("Disparando al jugador");

                    // Esperar el tiempo de retraso antes de permitir otro disparo
                    nextShootTime = Time.time + shootDelay;
                }
            }
            else
            {
                PerseguirJugador();
                Debug.Log("No hay línea de visión o esperando para disparar");
            }
        }
    }

    void ApuntarCabina()
    {
        if (target != null)
        {
            Vector3 direccionHaciaJugador = target.transform.position - cabinaController.transform.position;
            direccionHaciaJugador.y = 0;

            float anguloY = Mathf.Atan2(direccionHaciaJugador.z, direccionHaciaJugador.x) * Mathf.Rad2Deg;
            Quaternion rotacionFinal = Quaternion.Euler(-90, -anguloY, 90);

            cabinaController.transform.rotation = Quaternion.RotateTowards(cabinaController.transform.rotation, rotacionFinal, 2 * Time.deltaTime * 100);
        }
    }

    IEnumerator ApuntarYDisparar()
    {
        atacando = true;
        agente.isStopped = true;

        // Esperar hasta que la cabina apunte correctamente al jugador
        while (Quaternion.Angle(cabinaController.transform.rotation, Quaternion.Euler(-90, Mathf.Atan2(target.transform.position.z - cabinaController.transform.position.z, target.transform.position.x - cabinaController.transform.position.x) * Mathf.Rad2Deg, 90)) >= 0.5f)
        {
            yield return null;
        }

        ani.SetBool("walk", false);
        ani.SetBool("run", false);
        ani.SetBool("attack", true);

        // Asegurarse de que el disparo solo ocurra una vez por llamada
        yield return new WaitForSeconds(shootDelay);

        ani.SetBool("attack", false);
        atacando = false;
        agente.isStopped = false;
    }

    bool HayLineaDeVision()
    {
        RaycastHit hit;
        Vector3 origenRaycast = cabinaController.transform.position + Vector3.up * 1.0f; // Ajusta la altura si es necesario
        Vector3 direccionHaciaJugador = (target.transform.position - origenRaycast).normalized;

        Debug.DrawLine(origenRaycast, target.transform.position, Color.red);
        if (Physics.Raycast(origenRaycast, direccionHaciaJugador, out hit, shootRange))
        {
            if (hit.transform.CompareTag("PlayerTank"))
            {
                Debug.Log("Línea de visión clara hacia el jugador");
                return true;
            }
            else
            {
                Debug.Log("Raycast golpeó: " + hit.transform.name);
            }
        }
        return false;
    }

    void PerseguirJugador()
    {
        ani.SetBool("walk", false);
        ani.SetBool("run", true);
        ani.SetBool("attack", false);
        agente.isStopped = false;
        agente.SetDestination(target.transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explotar();
        }
    }

    public void Explotar()
    {
        Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Debug.Log("Explosion");
        ExplosionClip.PlayExplosionSound();
        Destroy(gameObject);
        if (manager != null) // Asegúrate de que el manager no sea nulo
        {
            manager.SumarKills();
        }
    }

    public void Final_Ani()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > shootRange + 0.2f)
        {
            ani.SetBool("attack", false);
        }

        atacando = false;
    }
}
