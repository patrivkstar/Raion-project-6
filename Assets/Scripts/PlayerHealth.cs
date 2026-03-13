using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health = 100;

    public Image healthFill; // UI health bar

    public float knockbackForce = 7f;
    private bool invulnerable = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }

        UpdateHealthBar();
    }

    public void TakeDamage(int amount, Vector2 enemyPos)
    {
        if (invulnerable) return;

        health -= amount;
        Debug.Log("Darah Player berkurang! Sisa: " + health);

        UpdateHealthBar(); // update UI

        if (rb != null)
        {
            Vector2 knockDir = ((Vector2)transform.position - enemyPos).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);
        }

        if (health <= 0) Die();
        else StartCoroutine(Iframe());
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = (float)health / maxHealth;
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
    }
}