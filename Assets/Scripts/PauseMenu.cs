using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;

    public Slider volumeSlider;
    public Slider brightnessSlider;

    public static bool isPaused = false; 

    public void ToggleMenu()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu(); 
        }
    }

    public void PauseGame()
    {
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f; 
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f; 
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMain()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void AdjustBrightness(float brightness)
    {
        RenderSettings.ambientLight = new Color(brightness, brightness, brightness);
    }
}