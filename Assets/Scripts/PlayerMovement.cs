using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator; 
    
    private Vector2 movement;
    private bool isFacingRight = true;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Cek arah jalan dan jalankan fungsi Flip jika arahnya berlawanan
        if (movement.x > 0 && !isFacingRight) 
        {
            Flip();
        }
        else if (movement.x < 0 && isFacingRight) 
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        // Membalikkan status arah
        isFacingRight = !isFacingRight;
        
        
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}