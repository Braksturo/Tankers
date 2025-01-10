using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbusto : MonoBehaviour
{
    public float opacidadReducida = 0.5f; // Opacidad reducida cuando el jugador está en contacto
    private Material materialArbusto; // Referencia al material del arbusto
    private Color colorOriginal; // Color original del material del arbusto

    void Start()
    {
        // Obtén el material del arbusto
        Renderer renderer = GetComponent<Renderer>();
        materialArbusto = renderer.material;

        // Guarda el color original del material
        colorOriginal = materialArbusto.color;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            // Reduce la opacidad del material del arbusto
            Color colorModificado = materialArbusto.color;
            colorModificado.a = opacidadReducida;
            materialArbusto.color = colorModificado;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale de contacto es el jugador
        if (other.CompareTag("Player"))
        {
            // Restaura la opacidad del material del arbusto al valor original
            materialArbusto.color = colorOriginal;
        }
    }
}
