using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Kalah();
        }
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Kalah();
        }
    }

    void Kalah()
    {
        Debug.Log("Player Mati!");
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}