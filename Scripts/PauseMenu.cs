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


    public Button yesButton; // Referencia al bot�n "S�"
    public Button noButton; // Referencia al bot�n "No"

    public GameObject optionsPanel; // Referencia al panel de opciones
    public GameObject controlsPanel;
    public Button controlsButton;
    public Button controlsButton2;

    private bool isPaused = false; // Estado del juego (pausado o no)

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        // Aseg�rate de que el panel est� desactivado al inicio
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);

        // A�adir listeners a los botones
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
        // Aqu� puedes cargar la escena del men� principal
        // Aseg�rate de que tienes una escena llamada "MenuPrincipal" en tu build settings
        SceneManager.LoadScene("MainMenu");
    }

    

    public void Options()
    {
        // Alternar la visibilidad del panel de opciones
        optionsPanel.SetActive(!optionsPanel.activeSelf);

        // Restablecer el estado del bot�n de opciones
        StartCoroutine(ResetButtonState());
    }

    public void Controls()
    {
        // Alternar la visibilidad del panel de opciones
        controlsPanel.SetActive(!controlsPanel.activeSelf);

        // Restablecer el estado del bot�n de opciones
        StartCoroutine(ResetButtonState());
    }

    private IEnumerator ResetButtonState()
    {
        // Esperar un frame para que el bot�n registre el cambio de estado
        yield return null;
        // Restablecer el estado del bot�n seleccion�ndolo y luego deseleccion�ndolo
        EventSystem.current.SetSelectedGameObject(null);
    }
}
