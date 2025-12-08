using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour, Controls.IPauseMenuActions
{
    [SerializeField] private bool paused;
    [SerializeField] private GameObject pauseMenu;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            paused = !paused;
            Time.timeScale = paused ? 0 : 1;
            pauseMenu.SetActive(paused);
        }
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        GameSeed.Initialize(GameSeed.Seed);
        // load active Scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void BackToMenu()
    {
        Time.timeScale = 1;
        // go to the first scene (menu)
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
