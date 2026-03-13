using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 10;
    private EnemyAI ai;

    void Start()
    {
       
        ai = GetComponent<EnemyAI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (ai != null && !ai.isStunned)
            {
                PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damageAmount, transform.position);
                }
            }
        }
    }
    
 
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ai != null && !ai.isStunned)
            {
                PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    ph.TakeDamage(damageAmount, transform.position);
                }
            }
        }
    }
}