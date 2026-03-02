using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5f;
    public GameObject slashPrefab;
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera cam;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update() {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

       
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime) {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    void Attack() {
     
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 attackDir = (mousePos - transform.position).normalized;

   
        Vector3 spawnPos = transform.position + (Vector3)attackDir * 1.5f;
        
        
        float angle = Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0, 0, angle - 90); // -90 agar tegak lurus

        GameObject slash = Instantiate(slashPrefab, spawnPos, rot);
        Destroy(slash, 0.2f); 
    }
}

