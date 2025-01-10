using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MineExplosion : MonoBehaviour
{
    EnemyTankController enemyTank;

    public float explosionDelay = 3f;
    public float explosionRadius = 5f;
    public LayerMask destructibleLayer;
    public Text countdownText;
    public Text countdownText2;
    public Material material1;
    public Material material2;
    public float blinkInterval = 0.5f;
    private float countdown;
    private Renderer mineRenderer;
    public GameObject ExplosionVFX;

    void Start()
    {
        enemyTank = FindObjectOfType<EnemyTankController>();
        countdown = explosionDelay;
        UpdateCountdownText();
        UpdateCountdownText2();
        InvokeRepeating("UpdateCountdown", 1f, 1f);
        Invoke("Explode", explosionDelay);
        StartCoroutine(BlinkMaterial());
        mineRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        UpdateCountdownTextPosition();
    }

    void UpdateCountdown()
    {
        countdown -= 1f;
        UpdateCountdownText();
        UpdateCountdownText2();
    }

    void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.Round(countdown).ToString();
        }
    }

    void UpdateCountdownText2()
    {
        if (countdownText != null)
        {
            countdownText2.text = Mathf.Round(countdown).ToString();
        }
    }

    void UpdateCountdownTextPosition()
    {
        if (countdownText != null)
        {
            // Posiciona el texto justo encima de la mina
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            countdownText.transform.position = screenPos + new Vector3(0, 30, 0);
            countdownText2.transform.position = screenPos + new Vector3(2, 30, 1);
        }
    }

    IEnumerator BlinkMaterial()
    {
        while (true)
        {
            mineRenderer.material = material1;
            yield return new WaitForSeconds(blinkInterval);
            mineRenderer.material = material2;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void Explode()
    {
        Instantiate(ExplosionVFX, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, destructibleLayer);
        foreach (Collider collider in colliders)
        {
            BoxController boxController = collider.GetComponent<BoxController>();
            if (boxController != null)
            {
                boxController.Explotar();
            }

            EnemyTankController enemyTank = collider.GetComponent<EnemyTankController>();
            if (enemyTank != null)
            {
                enemyTank.Explotar();
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
