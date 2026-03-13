using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void Start()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
        
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Kalah();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Kalah();
        }
    }

    void Kalah()
    {
        if (gameOverMenu != null)
        {
            Debug.Log("Player Mati!");
            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Debug.LogWarning("gameOverMenu belum dipasang di Inspector!");
        }
    }
}