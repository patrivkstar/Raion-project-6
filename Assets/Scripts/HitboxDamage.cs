using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    public int damageAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDummy enemy = other.GetComponent<EnemyDummy>();
            if (enemy != null) enemy.TakeDamage(damageAmount);
        }
    }
}