using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    private LevelManager levelManager;


    public Button yesButton; // Referencia al botón "Sí"
    public Button noButton; // Referencia al botón "No"

    public GameObject optionsPanel; // Referencia al panel de opciones
    public GameObject controlsPanel;
    public Button controlsButton;
    public Button controlsButton2;

    private bool isPaused = false; // Estado del juego (pausado o no)

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        // Asegúrate de que el panel esté desactivado al inicio
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        // Añadir listeners a los botones
        //yesButton.onClick.AddListener(OnYesButtonClick);
        //noButton.onClick.AddListener(OnNoButtonClick);
    }

    private void Update()
    {
        // Detectar si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cambiar la visibilidad del panel
            optionsPanel.SetActive(true);

            if (!isPaused && !levelManager.isCountdownActive)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1f; // Reanudar el juego
        isPaused = false;
    }

    public void OnYesButtonClick()
    {
        // Aquí puedes cargar la escena del menú principal
        // Asegúrate de que tienes una escena llamada "MenuPrincipal" en tu build settings
        SceneManager.LoadScene("MainMenu");
    }

    

    public void Options()
    {
        // Alternar la visibilidad del panel de opciones
        optionsPanel.SetActive(!optionsPanel.activeSelf);

        // Restablecer el estado del botón de opciones
        StartCoroutine(ResetButtonState());
    }

    public void Controls()
    {
        // Alternar la visibilidad del panel de opciones
        controlsPanel.SetActive(!controlsPanel.activeSelf);

        // Restablecer el estado del botón de opciones
        StartCoroutine(ResetButtonState());
    }

    private IEnumerator ResetButtonState()
    {
        // Esperar un frame para que el botón registre el cambio de estado
        yield return null;
        // Restablecer el estado del botón seleccionándolo y luego deseleccionándolo
        EventSystem.current.SetSelectedGameObject(null);
    }
}
