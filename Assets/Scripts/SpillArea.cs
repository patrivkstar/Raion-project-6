using UnityEngine;

public class SpillArea : MonoBehaviour
{
    public float lifetime = 4f;
    public int damagePerSecond = 10;
    private float damageTimer;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f)
            {
                other.GetComponent<PlayerMovement>().ReceiveDamage(damagePerSecond);
                damageTimer = 0f;
            }
        }
    }
}