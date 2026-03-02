using UnityEngine;

public class EnemyAI : MonoBehaviour {
    public float speed = 3f;
    public int health = 3;
    private Transform player;

    void Start() {
        // Cari player saat musuh muncul
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() {
        if (player != null) {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    // Terkena tebasan pedang
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("PlayerAttack")) { // Nanti ubah tag prefab Slash jadi ini
            health--;
            if (health <= 0) Destroy(gameObject);
        }
    }
}