using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public bool isCountdownActive = true;

    AudioSource audioSource;
    public AudioClip countdownClip;
    

    public float coinsWin = 20;
    public float killsWin = 12;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI coinText2;
    public TextMeshProUGUI mineText;
    public TextMeshProUGUI mineText2; // Referencia al TextMeshProUGUI para mostrar las minas
    public int enemyKills = 0;
    public int coins = 0;
    public int coins2 = 0;
    public int mine = 3; // Usar int para la cantidad de minas

    public string LoseScene = "MainMenu"; // Nombre de la escena a cargar
    public string WinScene;

    // Variables para la cuenta atrás
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI countdownText2; // Referencia al TextMeshProUGUI para mostrar la cuenta atrás
    public GameObject player; // Objeto que representa al jugador o el inicio del juego
    private TankMov tankMov; // Referencia al script del controlador del jugador

    // Variables para el fundido y el mensaje
    public Image fadeImage; // Referencia a la imagen de fundido
    public TextMeshProUGUI destroyedText; // Referencia al texto de "You have been destroyed"
    public TextMeshProUGUI winText;
    public TextMeshProUGUI winText2;
    public string winTextstring;// Referencia al texto de "You have been destroyed"
    public float fadeDuration = 1.0f; // Duración del fundido

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        mine = 3;
        coinText.text = coins.ToString();
        coinText2.text = coins2.ToString();
        mineText.text = mine.ToString();
        mineText2.text = mine.ToString();
        tankMov = player.GetComponent<TankMov>();
        tankMov.enabled = false;
        StartCoroutine(StartCountdown());
        StartCoroutine(StartCountdown2());

        destroyedText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (enemyKills >= killsWin && coins >= coinsWin)
        {
            LevelWin();
        }
    }

    public void SumarCoins()
    {
        audioSource.Play();
        coins += 1;
        coinText.text = coins.ToString();
        coins2 += 1;
        coinText2.text = coins2.ToString();
    }

    public void SumarKills()
    {
        enemyKills += 1;
    }

    public void RestarMina()
    {
        mine -= 1;
        mineText.text = mine.ToString();
        mineText2.text = mine.ToString(); // Actualizar el texto del TextMeshProUGUI cada vez que se reste una mina
    }

    public void SumarMina()
    {
        mine += 1;
        mineText.text = mine.ToString();
        mineText2.text = mine.ToString(); // Actualizar el texto del TextMeshProUGUI cada vez que se reste una mina
    }

    IEnumerator StartCountdown()
    {
        int countdownTime = 3;
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        countdownText.text = "TANKERS!";
        yield return new WaitForSeconds(1); // Espera un segundo más para mostrar "TANKERS!"
        countdownText.gameObject.SetActive(false); // Desactivar el texto de cuenta atrás
        tankMov.enabled = true; // Activar el movimiento del jugador

        isCountdownActive = false;

    }

    IEnumerator StartCountdown2()
    {
        int countdownTime = 3;
        while (countdownTime > 0)
        {
            countdownText2.text = countdownTime.ToString();
            yield return new WaitForSeconds(1);
            countdownTime--;
        }
        countdownText2.text = "TANKERS!";
        yield return new WaitForSeconds(1); // Espera un segundo más para mostrar "TANKERS!"
        countdownText2.gameObject.SetActive(false); // Desactivar el texto de cuenta atrás
        tankMov.enabled = true; // Activar el movimiento del jugador

        isCountdownActive = false;
    }

    public void OnPlayerDestroyed()
    {
        StartCoroutine(FadeToBlackAndWhite());
    }

    public void LevelWin()
    {
        StartCoroutine(FadeToBlackAndWhite2());
    }

    private IEnumerator FadeToBlackAndWhite()
    {
        // Fundido a blanco
        yield return StartCoroutine(Fade(Color.clear, Color.white, fadeDuration));
        // Fundido a negro
        yield return StartCoroutine(Fade(Color.white, Color.black, fadeDuration));

        // Mostrar mensaje
        destroyedText.gameObject.SetActive(true);
        destroyedText.text = "You have been destroyed";

        // Aquí puedes añadir un retraso antes de cambiar de escena o realizar otra acción
        yield return new WaitForSeconds(2.0f);

        // Cambiar de escena después del mensaje
        SceneManager.LoadScene(LoseScene);
    }

    private IEnumerator FadeToBlackAndWhite2()
    {
        

        // Mostrar mensaje
        winText.gameObject.SetActive(true);
        winText.text = winTextstring;
        winText2.text = winTextstring;

        // Aquí puedes añadir un retraso antes de cambiar de escena o realizar otra acción
        yield return new WaitForSeconds(2.0f);

        // Cambiar de escena después del mensaje
        SceneManager.LoadScene(WinScene);
    }

    private IEnumerator Fade(Color from, Color to, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            fadeImage.color = Color.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = to;
    }
}
