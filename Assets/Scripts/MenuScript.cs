using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject creditsScreen;
    private bool creditsActive = false;
    public GameObject tutorialScreen;
    private bool tutorialActive = false;
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsActive)
            {
                Credits();
            }
            else if (tutorialActive)
            {
                Tutorial();
            }
        }
    }

    public void PlayGame()
    {
        animator.SetBool("Play", true);
    }

    public void BackToMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("I QUIT THE GAME");
    }

    public void Credits()
    {
        if (creditsActive)
        {
            creditsScreen.gameObject.SetActive(false);
            creditsActive = false;
        }
        else
        {
            tutorialScreen.gameObject.SetActive(false);
            tutorialActive = false;
            creditsScreen.gameObject.SetActive(true);
            creditsActive = true;
        }
    }

    public void Tutorial()
    {
        if (tutorialActive)
        {
            tutorialScreen.gameObject.SetActive(false);
            tutorialActive = false;
        }
        else
        {
            creditsScreen.gameObject.SetActive(false);
            creditsActive = false;
            tutorialScreen.gameObject.SetActive(true);
            tutorialActive = true;
        }
    }
}
