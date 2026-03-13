using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Dash Settings")]
    public float dashSpeed = 25f; 
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    
    [Header("Status")]
    public bool isDashing = false;
    public bool isInvincible = false;

    private bool canDash = true;
    private TrailRenderer tr;
    private bool facingRight = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        anim = GetComponentInChildren<Animator>();
        if (tr != null) tr.emitting = false;
    }

    void Update()
    {
        if (isDashing) return;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (anim != null)
        {
            anim.SetBool("isWalking", moveInput.sqrMagnitude > 0);
        }
        
        if (moveInput.x > 0 && !facingRight) {
            Flip();
        } else if (moveInput.x < 0 && facingRight) {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && moveInput != Vector2.zero)
        {
            StartCoroutine(PerformDash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        rb.MovePosition(rb.position + moveInput.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;
        isInvincible = true;

        if (anim != null) anim.SetBool("isWalking", false);

        Vector2 dashDirection = moveInput.normalized;
        rb.linearVelocity = dashDirection * dashSpeed;

        if (tr != null) tr.emitting = true;

        yield return new WaitForSeconds(dashDuration);

        if (tr != null) tr.emitting = false;
        isDashing = false;
        isInvincible = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1; 
        transform.localScale = scaler;
    }

    public void ReceiveDamage(int damage)
    {
        if (isInvincible) return;
        Debug.Log("Player hit!");
    }
}