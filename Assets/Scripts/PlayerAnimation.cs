using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer sr;

    public Sprite[] idle;
    public Sprite[] walk;

    public float animSpeed = 0.12f;

    int frame = 0;
    float timer = 0;

    bool walking = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= animSpeed)
        {
            timer = 0;

            if(walking)
            {
                if(walk == null || walk.Length == 0)
                    return;

                frame = (frame + 1) % walk.Length;
                sr.sprite = walk[frame];
            }
            else
            {
                if(idle == null || idle.Length == 0)
                    return;

                frame = (frame + 1) % idle.Length;
                sr.sprite = idle[frame];
            }
        }
    }

    public void SetWalking(bool state)
    {
        if(walking != state)
        {
            walking = state;
            frame = 0;
        }
    }

    public void Flip(bool left)
    {
        sr.flipX = left;
    }
}