using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackCooldown = 0.4f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer >= attackCooldown)
        {
            Attack();
            timer = 0;
        }
    }

    void Attack()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position);
        direction.Normalize();

        Vector3 spawnPos = transform.position + (Vector3)direction * 0.7f;

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        proj.GetComponent<Projectile>().SetDirection(direction);
    }
}