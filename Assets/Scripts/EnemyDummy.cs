using UnityEngine;
using System.Collections;

public class EnemyDummy : MonoBehaviour
{
    public int maxHealth = 30;
    private int currentHealth;
    private SpriteRenderer sr;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(HitFlash());
        if (currentHealth <= 0) Destroy(gameObject);
    }

    IEnumerator HitFlash()
    {
        if(sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }
}