using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoHome()
    {
        Time.timeScale = 1f;

        // saves before going back to title
        SaveManager.SaveGame();

        SceneManager.LoadSceneAsync(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        //save before quitting
        SaveManager.SaveGame();

        Debug.Log("Quit Game");

        Application.Quit();
    }
}
