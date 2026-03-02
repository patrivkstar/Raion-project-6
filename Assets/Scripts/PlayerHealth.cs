using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    private int hp;

    [Header("UI Reference")]
    public Slider healthSlider;

    void Start()
    {
        hp = maxHP;
        healthSlider.maxValue = maxHP;
        healthSlider.value = hp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        healthSlider.value = hp; 

        if(hp <= 0) Die();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}