using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float knockbackForce = 5f;
    public float stunDuration = 0.5f; 
    
    private int currentHealth;
    private SpriteRenderer sr;
    private EnemyAI aiScript;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        aiScript = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        StopAllCoroutines(); 
        StartCoroutine(HitEffect());

        if (currentHealth <= 0) Die();
    }

    IEnumerator HitEffect()
    {
       
        if (aiScript != null) aiScript.isStunned = true;

      
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 knockbackDir = (transform.position - player.transform.position).normalized;
            rb.linearVelocity = Vector2.zero; 
            rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
        }

     
        for (int i = 0; i < 2; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

  
        yield return new WaitForSeconds(stunDuration);

        
        if (aiScript != null) aiScript.isStunned = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
    }
}