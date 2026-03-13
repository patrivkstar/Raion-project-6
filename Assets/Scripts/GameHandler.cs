using UnityEngine;

public partial class GameHandler : MonoBehaviour
{
    public GameObject gameOverMenu;

    public void PemicuKalah()
    {
        gameOverMenu.SetActive(true);

        Time.timeScale = 0f; 
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}