using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador
    public float followSpeed = 5f; // Velocidad de seguimiento de la c�mara

    private Vector3 initialOffset; // Offset inicial entre la c�mara y el jugador

    void Start()
    {
        // Calcula el offset inicial entre la c�mara y el jugador
        initialOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Calcula la nueva posici�n de la c�mara
        Vector3 targetPosition = target.position + initialOffset;

        // Solo sigue al jugador en el eje Z (adelante)
        targetPosition.x = transform.position.x;
        targetPosition.y = transform.position.y;

        // Interpola suavemente la posici�n de la c�mara hacia la nueva posici�n
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}