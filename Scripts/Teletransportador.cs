using UnityEngine;
using System.Collections;

public class Teletransportador : MonoBehaviour
{
    // Referencia al objeto de destino
    public Transform objetoDestino;

    // Referencia al prefab del efecto de partículas
    public GameObject efectoParticulasInicial;
    public GameObject efectoParticulasFinal;

    public float delayTP = 1;

    // Referencia al collider del teletransportador
    private Collider teletransportadorCollider;

    // Referencia al LevelManager
    private LevelManager levelManager;

    private void Start()
    {
        // Obtiene el collider del teletransportador
        teletransportadorCollider = GetComponent<Collider>();

        // Encuentra el LevelManager en la escena
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Este método se llama cuando otro objeto entra en el trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("PlayerTank"))
        {
            Instantiate(efectoParticulasInicial, transform.position, Quaternion.identity);
            // Inicia la coroutine para teletransportar al jugador con retraso
            StartCoroutine(TeletransportarConRetraso(other));
        }
    }

    // Coroutine para manejar el retraso y el teletransporte
    private IEnumerator TeletransportarConRetraso(Collider jugador)
    {
        // Desactiva el collider del teletransportador para evitar teletransportaciones repetidas
        teletransportadorCollider.enabled = false;

        // Desactiva el CharacterController del jugador
        CharacterController playerController = jugador.GetComponent<CharacterController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Espera el tiempo de delayTP
        yield return new WaitForSeconds(delayTP);

        // Teletransporta al jugador a la posición del objeto destino
        jugador.transform.position = objetoDestino.position;

        // Llamar al método SumarMina del LevelManager
        if (levelManager != null)
        {
            levelManager.SumarMina();
        }

        // Instancia el efecto de partículas en la nueva posición del jugador
        GameObject particulasFinal = Instantiate(efectoParticulasFinal, objetoDestino.position, Quaternion.identity);

        // Espera medio segundo para que las partículas sean visibles
        yield return new WaitForSeconds(0.5f);

        // Destruye las partículas finales
        Destroy(particulasFinal);

        // Reactiva el CharacterController del jugador después de un corto período de tiempo
        if (playerController != null)
        {
            playerController.enabled = true;
        }

        // Reactiva el collider del teletransportador después de un corto período de tiempo
        yield return new WaitForSeconds(0.5f);
        teletransportadorCollider.enabled = true;
    }
}
