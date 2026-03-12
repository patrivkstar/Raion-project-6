using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void ToggleMenu()
{
    bool isActive = pauseMenuUI.activeSelf;

    pauseMenuUI.SetActive(!isActive);

    if (!isActive)
    {
        Debug.Log("Game Pause");
        Time.timeScale = 0f;
    }
    else
    {
        Debug.Log("Game Resume");
        Time.timeScale = 1f;
    }
}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Game Keluar!");
        Application.Quit();
    }
}