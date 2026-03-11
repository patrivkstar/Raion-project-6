using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float knockbackForce = 5f;
    public float stunDuration = 0.5f;
    
    
    public string deathAnimationName = "Kroco_Death"; 

    private int currentHealth;
    private SpriteRenderer sr;
    private EnemyAI aiScript;
    private Rigidbody2D rb;
    private Animator anim; 
    private Collider2D col; 

    private bool isDead = false; 

    public Action OnDeath; 

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        aiScript = GetComponent<EnemyAI>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        col = GetComponent<Collider2D>(); 
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; 

        currentHealth -= damage;
        StopAllCoroutines(); 
        StartCoroutine(HitEffect());

        if (currentHealth <= 0) StartCoroutine(DieRoutine()); 
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

  
    IEnumerator DieRoutine()
    {
        isDead = true;

     
        if (aiScript != null) aiScript.enabled = false;
        if (GetComponent<EnemyDamage>() != null) GetComponent<EnemyDamage>().enabled = false;

        
        if (rb != null) rb.linearVelocity = Vector2.zero; 
        if (col != null) col.enabled = false; 

       
        if (anim != null)
        {
            anim.Play(deathAnimationName);
        
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        }
        else
        {
           
            yield return new WaitForSeconds(0.5f);
        }

        if (OnDeath != null) OnDeath.Invoke(); 
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack")) TakeDamage(1);
    }
}