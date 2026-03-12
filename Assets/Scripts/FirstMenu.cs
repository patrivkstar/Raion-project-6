using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu"); // nama scene menu
    }

    public void QuitGame()
    {
        Debug.Log("Game Keluar!");
        Application.Quit();
    }
}