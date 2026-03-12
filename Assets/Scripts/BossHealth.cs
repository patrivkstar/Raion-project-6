using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;
    private Vector3 originalPos;
    private Transform visual;

    void Start()
    {
        currentHealth = maxHealth;
    
        visual = transform.Find("BossVisual");
        
        if (visual != null)
        {
            originalPos = visual.localPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Darah Boss: " + currentHealth);

        if (visual != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShakeEffect());
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator ShakeEffect()
    {
        float elapsed = 0f;
        float duration = 0.2f;
        float magnitude = 0.1f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            visual.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        visual.localPosition = originalPos;
    }

    void Die()
    {
        
        Debug.Log("Boss Mati!");
        Destroy(gameObject);
    }
}