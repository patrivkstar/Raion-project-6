using UnityEngine;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Statistik")]
    public int maxHealth = 3;
    
    [Header("Animasi Mati (Drag File .anim ke Sini)")]
    public AnimationClip deathAnimationClip; 

    private int currentHealth;
    private bool isDead = false; 
    
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D col;
    private EnemyAI aiScript; 

    public Action onDeathCallback; 

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        aiScript = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        
        StopAllCoroutines();
        StartCoroutine(HitEffect());

        if (currentHealth <= 0)
        {
            StartCoroutine(DieRoutine());
        }
    }

    IEnumerator HitEffect()
    {
        if (aiScript != null) aiScript.isStunned = true;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
        if (aiScript != null) aiScript.isStunned = false;
    }

    IEnumerator DieRoutine()
    {
        isDead = true;

        if (aiScript != null) aiScript.enabled = false;
        if (rb != null) rb.linearVelocity = Vector2.zero;
        if (col != null) col.enabled = false;

        if (anim != null)
        {
            anim.Play("Kroco_Death", -1, 0f);
            
            if (deathAnimationClip != null) {
                yield return new WaitForSeconds(deathAnimationClip.length);
            } else {
                yield return new WaitForSeconds(1f);
            }
        }

        onDeathCallback?.Invoke(); 
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