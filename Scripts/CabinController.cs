using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinController : MonoBehaviour
{
    public Transform tank; // Referencia al transform del tanque (jugador principal)
    public float mouseSensitivity = 5f; // Sensibilidad del ratón para controlar la rotación de la cabina

    void LateUpdate()
    {
        // Obtener la posición del ratón en el mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            // Calcular la rotación hacia el objetivo solo en el eje Y
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - tank.position);
            Vector3 eulerAngles = targetRotation.eulerAngles;
            eulerAngles.x = -90f; // Mantener la rotación en el eje X a -90 grados
            targetRotation = Quaternion.Euler(eulerAngles);
            // Aplicar la rotación con suavidad
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * mouseSensitivity);
        }
    }
}




