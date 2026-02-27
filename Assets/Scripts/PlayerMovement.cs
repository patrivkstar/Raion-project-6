using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    public Sprite spriteDepan;
    public Sprite spriteBelakang;
    public Sprite spriteSamping;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        rb.linearVelocity = move * speed;

        GantiSprite(move);
    }

    void GantiSprite(Vector2 arah)
    {
        if (arah.y > 0)
        {
            sr.sprite = spriteBelakang;
        }
        else if (arah.y < 0)
        {
            sr.sprite = spriteDepan;
        }
        else if (arah.x != 0)
        {
            sr.sprite = spriteSamping;

            // flip kalau ke kiri
            if (arah.x < 0)
                sr.flipX = true;
            else
                sr.flipX = false;
        }
    }
}