using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // Importar EventSystems

public class SceneController : MonoBehaviour
{
    public string sceneName;
    public string sceneMenu;
    public TextMeshProUGUI textProgress;
    public GameObject LoadingCanva;
    public GameObject Canva;
    public GameObject optionsPanel; // Referencia al panel de opciones
    public GameObject controlsPanel;
    public GameObject missionPanel;
    public Button optionsButton; // Referencia al botón de opciones
    public Button controlsButton;
    public Button controlsButton2;
    public Button missionButton;
    public Button missionButton2;
    public Slider sliderProgress;
    public float currentPercent;

    private void Start()
    {
        LoadingCanva.SetActive(false);
        Canva.SetActive(true);
        optionsPanel.SetActive(false); // Asegúrate de que el panel de opciones esté oculto al inicio
        controlsPanel.SetActive(false);
        missionPanel.SetActive(false);
    }

    public void LoadGame()
    {
        //LoadingCanva.SetActive(true);
        //Canva.SetActive(false);
        //StartCoroutine(LoadScene(sceneName));
        SceneManager.LoadScene(sceneName);
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

    public void Mission()
    {
        // Alternar la visibilidad del panel de opciones
        missionPanel.SetActive(!missionPanel.activeSelf);

        // Restablecer el estado del botón de opciones
        StartCoroutine(ResetButtonState());
    }



    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneMenu);
    }

    public IEnumerator LoadScene(string nameToLoad)
    {
        textProgress.text = "Loading.. 00%";
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(nameToLoad);
        while (!loadAsync.isDone)
        {
            currentPercent = loadAsync.progress * 100 / 0.9f;
            textProgress.text = "Loading.. " + currentPercent.ToString("00") + "%";
            yield return null;
        }
    }

    private void Update()
    {
        sliderProgress.value = Mathf.MoveTowards(sliderProgress.value, currentPercent, 10 * Time.deltaTime);
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando el juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private IEnumerator ResetButtonState()
    {
        // Esperar un frame para que el botón registre el cambio de estado
        yield return null;
        // Restablecer el estado del botón seleccionándolo y luego deseleccionándolo
        EventSystem.current.SetSelectedGameObject(optionsButton.gameObject);
        EventSystem.current.SetSelectedGameObject(null);
    }
}



