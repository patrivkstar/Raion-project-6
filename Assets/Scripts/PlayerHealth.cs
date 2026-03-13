using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;

    public Image healthFill;
    public GameObject gameOverMenu;

    public float knockbackForce = 7f;
    private bool invulnerable = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        if (gameOverMenu != null) gameOverMenu.SetActive(false);

        UpdateHealthBar();
    }

    public void TakeDamage(int amount, Vector2 enemyPos)
    {
        if (invulnerable) return;

        health -= amount;
        Debug.Log("Darah Player berkurang! Sisa: " + health);

        UpdateHealthBar();

        if (rb != null)
        {
            Vector2 knockDir = ((Vector2)transform.position - enemyPos).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
        }

        if (health <= 0)
        {
            health = 0;
            UpdateHealthBar();
            Die();
        }
        else
        {
            StartCoroutine(Iframe());
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = (float)health / (float)maxHealth;
        }
    }

    IEnumerator Iframe()
    {
        invulnerable = true;
        if (sr != null)
        {
            for (int i = 0; i < 4; i++)
            {
                sr.color = new Color(1, 1, 1, 0.2f);
                yield return new WaitForSeconds(0.1f);
                sr.color = new Color(1, 1, 1, 1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
        }
        invulnerable = false;
    }

    void Die()
    {
        Debug.Log("Game Over");

        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void KeMenuUtama()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("FirstMenu");
    }
}