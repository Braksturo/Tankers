using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public Transform target; // Referencia al transform del jugador
    public float followSpeed = 5f; // Velocidad de seguimiento de la cámara

    private Vector3 initialOffset; // Offset inicial entre la cámara y el jugador

    void Start()
    {
        // Calcula el offset inicial entre la cámara y el jugador
        initialOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // Calcula la nueva posición de la cámara
        Vector3 targetPosition = target.position + initialOffset;

        // Solo sigue al jugador en el eje Z (adelante)
        targetPosition.x = transform.position.x;
        targetPosition.y = transform.position.y;

        // Interpola suavemente la posición de la cámara hacia la nueva posición
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}